const { Router } = require('express');
const router = Router();
const routeController = require('../routeController/controller');
const jwt = require('jsonwebtoken');
const User = require('../models/userModel');


// ======================== middleware to protect routes from users not logged in =================================
const authToken = (req, res, next) => {

    // extract the token from the cookie
    const token = req.cookies.jwt;

    // check if the token is present, if not redirect to the login page
    if (token) {
        // if the token is present, verify it
        jwt.verify(token, 'this is a very long and complicated thing to understand at the very moment in time', (err, decodedToken) => {
            // if the token is not verified, redirect to the login page, if not continue
            if (err) {
                res.redirect('/');
            }
            else {
                next();
            }
        })
    }
    else {
        res.redirect('/');
    }
}

// ====================================== check current user and get their details =================================
const checkCurrentUser = async (req, res, next) => {
    // get token
    const token = req.cookies.jwt;

    // check if token present
    if (token) {
        // if token present, verify it
        jwt.verify(token, 'this is a very long and complicated thing to understand at the very moment in time', async (err, decodedToken) => {
            if (err) {
                // if token not verified, set user to null
                res.locals.user = null
                next();
            }
            else {
                // if token verified, get user details and store them
                let user = await User.findById(decodedToken.id);
                res.locals.user = user;
                next()
            }
        })
    }
    else {
        const userEmail = req.body.email;
        User.findOne({userEmail})
        .then(respo => {
            res.locals.user = respo;
            next();
        })
    }
}

// to check if the user is registered
const checkRegistration = (req, res, next) => {
    const regToken = req.cookies.newReg;

    if (regToken) {
        jwt.verify(regToken, 'This is a string to create a token only for registered users', (err, decodedToken) => {
            if (err) {
                res.redirect('/');
            }
            else {
                next()
            }
        })
    }
    else {
        res.redirect('/');
    }
}

// apply user checking middleware to all get requests
router.get('*', checkCurrentUser);

// ===============================================    ROUTES    =================================================
router.get('/', routeController.home_get);
router.get('/logout', routeController.logout_post);
router.get('/register', routeController.reg_get);
router.get('/main', authToken, routeController.main_get);
router.post('/main', checkCurrentUser, routeController.login_post);
router.post('/regNew/:id', checkCurrentUser, routeController.main_post)
router.get('/profile', authToken, routeController.profile_get);
router.post('/general', routeController.general_post);
router.get('/general', checkRegistration, routeController.general_get);
router.post('/carPools',checkCurrentUser, routeController.search_post);
router.get('/carPools', authToken, routeController.carpools_get)
router.post('/join-pool/:id', authToken, routeController.join_post); 
router.put('/update/:id', checkCurrentUser, routeController.update_put);
router.delete('/delete/:id', checkCurrentUser, authToken, routeController.delete_account);
router.post('/view-joined/:id', checkCurrentUser, authToken, routeController.viewJoined_post);
router.delete('/deletePool/:id', authToken, routeController.delete_pool);
router.get('/created/:id', authToken, routeController.created_get);
router.get('/all', routeController.allPools_get);
router.get('/notLoggedIn', routeController.notIn_get);
router.get('/getPrevJoined/:id', authToken, routeController.prev_get);



// ===============================    REST API   =================================
router.get('/api/v1/allcar-pools', routeController.allPools_get); // get all listed car-pool opportunities
router.post('/api/v1/newUser', routeController.general_post); // create a new user
router.put('/api/v1/update/:userId', routeController.update_put), // make updates to an existing user
router.delete('/api/v1/deleteAccount/:userId', routeController.delete_account); // delete an existing account
router.get('/api/v1/joinedPool/:userId', routeController.viewJoined_post); // get all joined car-pool opportunities
router.get('/api/v1/deletePool/<carPoolId>-<userId>', routeController.delete_pool); // leave a joined car-pool opportunity
router.get('/api/v1/createdPools/:id', routeController.created_get); // get all user created car-pool opportunities (by user id)


module.exports = router;