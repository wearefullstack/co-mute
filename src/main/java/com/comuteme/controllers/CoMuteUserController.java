package com.comuteme.controllers;

import com.comuteme.domain.CoMuteUser;
import com.comuteme.repositories.CoMuteUserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class CoMuteUserController {
        @Autowired
        private CoMuteUserRepository coMuteUserRepository;
        @RequestMapping("/comuters")
        @CrossOrigin(origins = "http://localhost:3000")
        public Iterable<CoMuteUser> getCarpools(){
            return coMuteUserRepository.findAll();
        }
    }
