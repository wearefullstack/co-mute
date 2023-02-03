package io.emmanuel.comutefinal.Service;

import io.emmanuel.comutefinal.Repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;


@Service
public class UserService {

    @Autowired
    UserRepository repository;

    /*
    Used to return the userName by the id provided from db/repo
     */
    public String getUserName(int id){
        return repository.findByUserid(id).getName() ;

    }

    /*
        RETURN the logged in User's Id will be passed through different views and used in them for database/repo calls
  */
    public int getUserId(String email,String password){
        return repository.findByEmailAndPassword(email, password).getUserid();
    }

    /*
    The following methods  provide the user with the ability to update their details.


    public void updateUserName(String name,int id){
        repository.updateNameByUserid(name,id);
    }
    public void updateEmail(String email,int id){
        repository.updateEmailByUserid(email,id);
    }
    public void updateUserPhone(String phone,int id){
        repository.updatePhoneByUserid(phone,id);
    }
    public void updatePassword(String password,int id){
        repository.updatePasswordByUserid(password,id);
    }

     */
}
