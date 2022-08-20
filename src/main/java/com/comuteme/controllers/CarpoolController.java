package com.comuteme.controllers;

import com.comuteme.domain.Carpool;
import com.comuteme.domain.CoMuteUser;
import com.comuteme.domain.JoinCarPool;
import com.comuteme.repositories.CarPoolRepository;
import com.comuteme.repositories.CoMuteUserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
public class CarpoolController {

    @Autowired
    private CarPoolRepository carPoolRepository;
    @Autowired
    private CoMuteUserRepository coMuteUserRepository;
    @RequestMapping("/carpools")
    @CrossOrigin(origins = "http://localhost:3000")
    public Iterable<Carpool> getCarpools(){
        return carPoolRepository.findAll();
    }

    @PostMapping(path = "/joinpool",
    consumes = MediaType.APPLICATION_JSON_VALUE,
    produces = MediaType.APPLICATION_JSON_VALUE)
    @CrossOrigin(origins = "http://localhost:3000")
    public ResponseEntity<Carpool> joinPool(@RequestBody JoinCarPool joinCarPool) {
        CoMuteUser coMuteUser =  coMuteUserRepository.findByEmail(joinCarPool.getEmail());
        joinCarPool.getCarpool().getCoMuteUsers().add(coMuteUser);

        Carpool user = carPoolRepository.save(joinCarPool.getCarpool());
        if (user == null) {
            throw new IllegalArgumentException();
        } else {
            return new ResponseEntity<>(user, HttpStatus.CREATED);
        }
    }

}
