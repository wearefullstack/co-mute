package com.comuteme.domain;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import javax.persistence.*;
import java.util.List;


//@Column(nullable = false, unique = true)
//        private String coMuteUserName;  // will be used for authentication purposes
@Entity
@JsonIgnoreProperties({"hibernateLazyInitializer", "handler"})
public class CoMuteUser {
        @Id
        @GeneratedValue(strategy= GenerationType.AUTO)
        private long coMuteUserId;
        private String firstname;
        private String lastname;
        private String password;
        private String email;

        @JsonIgnore
        @OneToMany(cascade = CascadeType.ALL, mappedBy = "coMuteUsers")
        private List<Carpool> carpools;


    public CoMuteUser() {
    }

    public CoMuteUser(String firstname, String lastname, String password, String email) {
        this.firstname = firstname;
        this.lastname = lastname;
        this.password = password;
        this.email = email;
    }

    public long getCoMuteUserId() {
        return coMuteUserId;
    }

    public void setCoMuteUserId(long coMuteUserId) {
        this.coMuteUserId = coMuteUserId;
    }

    public String getFirstname() {
        return firstname;
    }

    public void setFirstname(String firstname) {
        this.firstname = firstname;
    }

    public String getLastname() {
        return lastname;
    }

    public void setLastname(String lastname) {
        this.lastname = lastname;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public List<Carpool> getCarpools() {
        return carpools;
    }

    public void setCarpools(List<Carpool> carpools) {
        this.carpools = carpools;
    }
}
