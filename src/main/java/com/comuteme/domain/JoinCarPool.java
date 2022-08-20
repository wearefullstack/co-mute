package com.comuteme.domain;

public class JoinCarPool {

    private String email;
    private Carpool carpool;

    public JoinCarPool() {
    }

    public JoinCarPool(String email, Carpool carpool) {
        this.email = email;
        this.carpool = carpool;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public Carpool getCarpool() {
        return carpool;
    }

    public void setCarpool(Carpool carpool) {
        this.carpool = carpool;
    }
}
