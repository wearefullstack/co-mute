package com.comuteme.controllers;

import com.comuteme.domain.Carpool;
import com.comuteme.domain.CoMuteUser;
import com.comuteme.domain.LoginUser;
import com.comuteme.repositories.CarPoolRepository;
import com.comuteme.repositories.CoMuteUserRepository;
import com.comuteme.securityconfig.LoginService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import static org.springframework.web.bind.annotation.RequestMethod.POST;

@RestController
public class LoginController {

    @Autowired
    private LoginService loginService;

    @Autowired
    private CoMuteUserRepository coMuteUserRepository;

    @Autowired
    private CarPoolRepository carPoolRepository;


    @PostMapping(path = "users/add",
            consumes = MediaType.APPLICATION_JSON_VALUE,
            produces = MediaType.APPLICATION_JSON_VALUE)
    @CrossOrigin(origins = "http://localhost:3000")
    public ResponseEntity<CoMuteUser> create(@RequestBody CoMuteUser newUser) {
        CoMuteUser user = coMuteUserRepository.save(newUser);
        if (user == null) {
            throw new IllegalArgumentException();
        } else {
            return new ResponseEntity<>(user, HttpStatus.CREATED);
        }
    }

    @PostMapping(path = "/createpool",
            consumes = MediaType.APPLICATION_JSON_VALUE,
            produces = MediaType.APPLICATION_JSON_VALUE)
    @CrossOrigin(origins = "http://localhost:3000")
    public ResponseEntity<Carpool> createPool(@RequestBody Carpool carpool) {
        Carpool user = carPoolRepository.save(carpool);
        if (user == null) {
            throw new IllegalArgumentException();
        } else {
            return new ResponseEntity<>(carpool, HttpStatus.CREATED);
        }
    }

    @RequestMapping(value = "/clogin",
            method = POST,
            consumes = MediaType.APPLICATION_JSON_VALUE,
            produces = MediaType.APPLICATION_JSON_VALUE)
    @ResponseBody
    public boolean authenticateCoMuteUser(@RequestBody LoginUser loginUser){
        return loginService.authenticateUser(loginUser.getEmail(), loginUser.getPassword());
    }
}
