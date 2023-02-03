package io.emmanuel.comutefinal.Service;


import io.emmanuel.comutefinal.Model.UserModel;
import io.emmanuel.comutefinal.Repository.CarPoolRepository;
import io.emmanuel.comutefinal.Repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class RegisterService {
    @Autowired
    UserRepository repository;

    /*
    Method used to add a new user to database/repo
     */
    public  boolean registerUser(String name,String surname, String phone, String email, String password ){
        boolean isSuccess = false;
        if(!(name.isEmpty()) && !(surname.isEmpty()) && !(email.isEmpty()) && !(password.isEmpty()) ){ //Check if the fields are not empty

            //Create user object
            UserModel newUser = new UserModel();
            newUser.setName(name);
            newUser.setEmail(email);
            newUser.setPhone(phone);
            newUser.setSurname(surname);
            newUser.setPassword(password);
            repository.save(newUser);

            isSuccess = true;
        }
        return  isSuccess;
    }
}
