package com.comuteme.securityconfig;

import com.comuteme.repositories.CoMuteUserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class LoginService {

    @Autowired
    private CoMuteUserRepository coMuteUserRepository;

    public boolean authenticateUser(String email, String password)
    {
        //todo find CoMuteUser from email, check saved password

        var retrievedCoMuteUser = coMuteUserRepository.findByEmail(email);
        if(password.contentEquals(retrievedCoMuteUser.getPassword()))
        {
            return true;
        }

        return false;
    }

}
