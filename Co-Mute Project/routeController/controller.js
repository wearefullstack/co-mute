const User = require("../models/userModel");
const CarPool = require("../models/carPool");
const jwt = require("jsonwebtoken");
const bcrypt = require("bcrypt");

const maxAge = 1 * 60 * 60;

const months = {
  1: "Jan",
  2: "Feb",
  3: "Mar",
  4: "Apr",
  5: "May",
  6: "Jun",
  7: "Jul",
  8: "Aug",
  9: "Sep",
  10: "Oct",
  11: "Nov",
  12: "Dec",
};

const generateToken = (id) => {
  return jwt.sign(
    { id },
    "this is a very long and complicated thing to understand at the very moment in time",
    { expiresIn: maxAge }
  );
};

const capFirstLetter = (str) => {
  return str.charAt(0).toUpperCase() + str.slice(1);
};

// =============================================    HANDLE ERRORS    ===================================== 
const errorHandler = (err) => {
  let errors = { email: "", password: "", redirect: ""};

  if (err.code === 11000) {
    errors.email = 'An account already exists for the email entered';
    return errors;
  }
  else if (err.message.includes('User validation failed')) {
    const objectProps = Object.values(err.errors)
    objectProps.forEach(({properties}) => {
      errors[properties.path] = properties.message;
    })
    return errors
  }
  else {
    if (err.message.includes('User')) {
      errors.email = err.message
    }
    else {
      errors.password = err.message
    }
    return errors;
  }
}

module.exports.home_get = (req, res) => {
  res.render("login(LP)");
};

module.exports.reg_get = (req, res) => {
  res.render("register", { title: "Register" });
};

// =======================      HANDLE USER AUTHENTICATION      ===========================
module.exports.login_post = async (req, res) => {
  try {
    const user = await User.login(req.body.email, req.body.password);


    // generate a token and attach it to a cookie
    const token = generateToken(user._id);
    res.cookie("jwt", token, { httpOnly: true, maxAge: maxAge * 1000 });

    res.status(200).json({email: "", password: "", redirect: '/main'});
  } 
  catch (err) {

    // handle the error
    const errors = errorHandler(err);
    res.json(errors)
  }
};

module.exports.main_get = (req, res) => {
  res.render("main", { title: "Main" });
};

// =======================      HANDLE CAR-POOL CREATION REQUEST      ===========================
module.exports.main_post = async (req, res, next) => {
  // get user ID
  const userId = req.params.id;

  const user = await User.findById(userId)

  let userCreatedPools = user.createdPools;

  CarPool.find()
  .then(pools => {

    let exists = false;

    if (pools.length !== 0) {
          // check if the car-pool is already registered
    for (let i=0; i < pools.length; i++) {
      if(pools[i].id === userId) {
        if (pools[i].days_available[0] === req.body.fromDate) {
          if (pools[i].departure_time === req.body.departureTime) {
            exists = true;
            break;
          }
        }
      }
    }
    }

    // if not registered, add it
    if (!exists) {
      const days = [req.body.fromDate, req.body.toDate];

      const carPoolDetails = new CarPool({
        id: userId,
        departure_time: req.body.departureTime,
        arrival_time: req.body.arrivalTime,
        origin: capFirstLetter(req.body.origin),
        days_available: days,
        destination: capFirstLetter(req.body.destination),
        available_seats: req.body.seats,
        owner: capFirstLetter(req.body.owner),
        notes: req.body.notes,
        dateJoined: ""
      });
    
      carPoolDetails
        .save()
        .then((response) => {

          User.findByIdAndUpdate({
            _id: userId
          },
          {
            createdPools: userCreatedPools + 1
          })
          .then(reee => {next()})

          res.json({alert: 'Car-pool successfully created!', poolClash: ""})
        })
        .catch((err) => console.log(err)); /*============================= */
    }
    else {
      res.json({poolClash: 'You have already registered a car-pool for that date and departure time!', redirect: ""})
    }
  })
};

module.exports.profile_get = (req, res) => {
  res.render("profile");
};

// =======================     HANDLE NEW USER REGISTRATION     ===========================
module.exports.general_post = (req, res) => {

    const UserDetails = new User({
      name: capFirstLetter(req.body.name),
      surname: capFirstLetter(req.body.surname),
      cell_number: req.body.number,
      email: req.body.email,
      password: req.body.password,
      createdPools: 0
    });

    const mail = req.body.email;

    const regToken = jwt.sign({mail}, 'This is a string to create a token only for registered users', { expiresIn: (365.24 * 24 * 60 * 60) * 50})
    res.cookie('newReg', regToken, { maxAge: (365.24 * 24 * 60 * 60 * 1000) * 50})
    
    UserDetails.save()
      .then((response) => {
        res.status(200).json({email: "", password: "", redirect: "/general"})
      })
      .catch((err) => {
        
        let errors = errorHandler(err)
        res.json(errors)});
};

module.exports.general_get = (req, res) => {
  res.render('general')
};

// =======================      DELETE TOKEN WHEN USER LOGS OUT     ===========================
module.exports.logout_post = (req, res) => {
  res.cookie("jwt", "", { maxAge: 1 });
  res.redirect("/");
};

// =======================================      HANDLE CAR-POOL SEARCH REQUEST      ==========================================
module.exports.search_post = (req, res) => {

  let avail_carPools = [];

  CarPool.find()
    .sort({ createdAt: -1 })
    .then((response) => {
      response.forEach((pool) => {
        // get user requested date
        const userDate = new Date(req.body.day);

        // get days
        const fromDate = new Date(pool.days_available[0]);
        const toDate = new Date(pool.days_available[1]);

        // compare dates
        if (userDate >= fromDate && userDate <= toDate) {
          // add the car-pool to the list
          avail_carPools.push(pool);
        }
      });
      res.render("carPools", {
        carPools: avail_carPools,
        title: "Available Car-pools",
        months,
      });
    })
    .catch((err) => {
      console.log(err);
    });
};

module.exports.carpools_get = (req, res) => {
  res.redirect("/main");
};

// ====================================================   HANDLE JOIN CAR-POOL REQUEST  ============================================
module.exports.join_post = async (req, res) => {

  // get the route parameter from the request
  const routeParam = req.params.id;

  // split the route parameter to get id for joined car-pool and user
  const arrayOfIds = routeParam.split("-");

  // Ids of joined car-pool and user
  const userId = arrayOfIds[1];
  const carPoolId = arrayOfIds[0];

  // get the joined car-pool details from database
  const joinedCarPool = await CarPool.findById(carPoolId);
  const userr = await User.findById(userId);

  const prevPools = userr.prevJoined;

  // get car-pool details
  const poolDepartureTime = joinedCarPool.departure_time;

  // const poolArrivalTime = joinedCarPool.arrival_time;
  const poolAvailability = joinedCarPool.days_available;


  let date = new Date();

  const date_joined = date.getDate() + ' ' + months[date.getMonth()+1] + ' ' + date.getFullYear();

  // check if there is capacity
  if (joinedCarPool.available_seats !== 0) {

  // find user and save the joined car-pool opportunity
  User.findById(userId)
  .then((resp) => {

    const userjoinedPools = resp.joined_carPools;

    let match = 0;
    let available = true;

    // check if user has already joined the car-pool
    if (userjoinedPools.length !== 0) {

      userjoinedPools.forEach(pool => {
        if (pool !== "") {
          if (pool._id.toString() === carPoolId) {
            match = match + 1;
          }
        }
        

        if((new Date(poolAvailability[0]) >= new Date(pool.days_available[0])) || (new Date(poolAvailability[1]) <= new Date(pool.days_available[1]))) {
          if (poolDepartureTime === pool.departure_time) {
            available = false;
          }
        }
      })
    }

    // if joined send back alert message, if not add car-pool
    if (match !== 0) {
      res.json({cannotJoin: 'You have already joined this car-pool!', joined: "", full: ""})
    }
    else if (match === 0 && available === true) {

      joinedCarPool.dateJoined = date_joined;

      // add the joined car-pool to the list
      userjoinedPools.push(joinedCarPool);
      prevPools.push(joinedCarPool)

      User.findByIdAndUpdate(
        {
          _id: userId
        },
        {
          joined_carPools: userjoinedPools,
          prevJoined: prevPools
        }
      )
      .then(respon => {

        let seats_available = joinedCarPool.available_seats - 1;

        CarPool.findByIdAndUpdate(
          {
            _id: carPoolId
          },
          {
            available_seats: seats_available
          }
        )
        .then(respon => {
          res.status(200).json({joined: 'Car-pool successfully joined.', full: ""})
        })
      })
    }
    else {
      res.json({clash: 'The time-frame of the car-pool you want to join clashes with one of the car-pools you have already joined',
                full: "",
                joined: "",
                cannotJoin: ""})
    }
  });
  }
  else {
    res.json({full: 'No more seats available!', joined: ""})
  }
}

// =================================      HANDLE UPDATE DETAILS REQUESTS      =====================================
module.exports.update_put = async (req, res) => {
  const userId = req.params.id;

  try {

    User.findById(userId)
    .then(async (r) => {
      let newName = r.name;
      let newSurname = r.surname;
      let newNumber = r.cell_number;
      let newPassword = r.password;
      let pwsNoMatch = "";
      console.log(req.body.current_password)
      const checkMatch = await bcrypt.compare(req.body.current_password, r.password);

      if (checkMatch === true) {
        if (req.body.new_password !== "") {
          if (req.body.new_password === req.body.confirm_password) {
            // hash the new password
            newPassword = await User.hashPassword(req.body.new_password);
          } else {
            pwsNoMatch = "Passwords don't match!"
          }
        }
        if (req.body.name !== "") {
          newName = req.body.name;
        }
        if (req.body.surname !== "" ) {
          newSurname = req.body.surname; 
        }
        if (req.body.phone !== "") {
          newNumber = req.body.phone;
        }
  
        // update user details
        User.findOneAndUpdate(
          { _id: userId },
          {
            name: capFirstLetter(newName),
            surname: capFirstLetter(newSurname),
            cell_number: newNumber,
            password: newPassword,
          }
        ).then((resp) => {
          if (pwsNoMatch !== "") {
            res.json({alert: '', noMatch: "", pwsNoMatch})
          }
          else {
            res.status(200).json({alert: 'Your details have been successfully updated', noMatch: ""})
          }
        });
      } else {
        res.status(401).json({noMatch: 'Incorrect current password', alert: "", pwsNoMatch})
      }
    })
  } catch (err) {
    res.status(400).json({err})
  }
};

// =======================      HANDLE DELETE ACCOUNT REQUEST      ===========================
module.exports.delete_account = (req, res) => {
  const userId = req.params.id;

  // find user and delete
  User.findByIdAndDelete(userId).then((resp) => {
    // delete user token
    res.cookie("jwt", "", { maxAge: 1 });

    // send the response back to the javascript AJAX request
    res.status(201).json({ redirect: "/" });
  });
};

// =================================      HANDLE VIEWING OF JOINED CAR-POOLS     =====================================
module.exports.viewJoined_post = (req, res) => {
  const userId = req.params.id;

  User.findById(userId).then(response => {
    const avail_car_pools = response.joined_carPools;

    res.status(200).json({ availCars: avail_car_pools });
  })
  .catch(err => res.status(400).json({err}))
}

// =================================      HANDLE DELETION OF JOINED CAR-POOLS     =====================================
module.exports.delete_pool = async (req, res) => {
  const id = req.params.id;
  const arrId = id.split('-');

  const poolId = arrId[0];
  const userId = arrId[1];

  let array = [];

  const pool = await CarPool.findById(poolId);

  let seats_avail = 0;

  User.findById(userId)
  .then(resp => {
    let joinedPools = resp.joined_carPools;

    if (joinedPools.length !== 0 && joinedPools[0] !== "") {
      let arr = joinedPools.filter(pool => {
        return pool._id.toString() !== poolId
      })
      array = arr.slice()
    }

    seats_avail = pool.available_seats + 1;

    User.findOneAndUpdate(
      {
        _id: userId
      },
      {
        joined_carPools: array
      }
    )
    .then(user => {
    })

    CarPool.findOneAndUpdate(
      {
        _id: poolId
      },
      {
        available_seats: seats_avail
      }
    )
    .then(responsee => {
      res.status(200).json({responsee})
    })
  })
}

// =================================      HANDLE VIEWING OF CREATED CAR-POOLS     =====================================
module.exports.created_get = (req, res) => {
  const id = req.params.id;

  let poolsCreated = [];

  CarPool.find()
  .then(resp => {
    resp.forEach(cars => {
      if (cars.id === id) {
        poolsCreated.push(cars)
      }
    })
    res.status(200).json({pool: poolsCreated})
  })
  .catch(err => res.status(404).json({err}))
}

module.exports.allPools_get = (req, res) => {
  CarPool.find().sort({createdAt: -1})
  .then(all => {
    res.status(200).json({allPools: all})
  })
  .catch(err => res.status(404).json({err}))
}

// ======================================   HANDLE CAR-POOL SEARCH FROM USERS NOT LOGGED IN   ==============================
module.exports.notIn_get = (req, res) => {
  let avail_carPools = [];

  CarPool.find()
    .sort({ createdAt: -1 })
    .then((response) => {
      response.forEach((pool) => {
        
        // get user requested date
        const userDate = new Date(req.body.day);

        // get days
        const fromDate = new Date(pool.days_available[0]);
        const toDate = new Date(pool.days_available[1]);

        // compare dates
        if (userDate >= fromDate && userDate <= toDate) {
          // add the car-pool to the list
          avail_carPools.push(pool);
        }
      });
      res.status(200).render("notLoggedIn", {
        carPools: avail_carPools,
        months,
      });
    })
    .catch((err) => {
      res.status(400).json({noCars: 'There was an error fetching opportunities'})
    });
};

// ================================= GETTING PREVIOUSLY JOINED CAR-POOLS ==================================
module.exports.prev_get = (req, res) => {
  const userId = req.params.id;

  User.findById(userId)
  .then(response => {
    res.status(200).json({prev: response.prevJoined})
  })
  .catch(err => {
    res.status(400).json({noData: 'There was an error fetching your requested data'})
  })
}