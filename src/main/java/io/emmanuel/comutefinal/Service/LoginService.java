package io.emmanuel.comutefinal.Service;

import io.emmanuel.comutefinal.Model.UserModel;
import io.emmanuel.comutefinal.Repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class LoginService {

    @Autowired
    UserRepository repository;

    /*
        Method returns TRUE if a userModel is returned after
        Executing Query statement findbyEmail.. meaning a User
        exist and has been found,
        OTHERWISE returns FALSE

     */
    public Boolean authenticateLogin(String email, String password){
        UserModel aUser = repository.findByEmailAndPassword(email, password);
        if(aUser ==null){
            return false;
        }else {
            return true;
        }
    }


}
