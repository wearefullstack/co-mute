package io.emmanuel.comutefinal.Service;

import io.emmanuel.comutefinal.Model.CarPool;
import io.emmanuel.comutefinal.Model.UserModel;
import io.emmanuel.comutefinal.Repository.CarPoolRepository;
import io.emmanuel.comutefinal.Repository.UserRepository;
import io.emmanuel.comutefinal.dto.CarpoolRequest;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
    /*
    * Cannot register to Car pool opportunities with over lapping time frames method
    *
    * Implementation: check if there is an existing car pool the user registered to,
     If not then register the user else check if the car pool is on
     the same day if it is then check the duration of trip from departure time and arrive
     times of both the car pool opportunities.
     */

/*
There should be a leave car pool opportunity method which remove the car pool from the
List<CarPool> JoinedCar pool
 */
@Service
public class CarpoolService {
    @Autowired
    CarPoolRepository carRepository;

    @Autowired
    UserService userService;

    public List<CarPool> listAllCarPool(){
        return carRepository.findAll();
    }

    /*
    This method should be used to return the list of Car pool(Joined)
    associated by user Id

    public List<CarPool> listjoinedCarPool(int id){
        return carRepository.findBy();
    }

     */



    public String setRepository(CarpoolRequest carPoolInfo,int id) {

        //Create car pool object and set data
        CarPool newCarpoolObj = new CarPool();
        newCarpoolObj.setArrTime(carPoolInfo.getArrTime());
        newCarpoolObj.setDepTime(carPoolInfo.getDepTime());
        newCarpoolObj.setDestination(carPoolInfo.getDestination());
        newCarpoolObj.setOrigin(carPoolInfo.getOrigin());
        ///newCarpoolObj.setOwner(userService.getUserName(id));
        newCarpoolObj.setNumDayAvail(carPoolInfo.getNumDayAvail());
        newCarpoolObj.setNumAvailSeats(carPoolInfo.getNumAvailSeats());
        newCarpoolObj.setNotes(carPoolInfo.getNotes());
        carRepository.save(newCarpoolObj);
        return "Successfully created";
    }
}
