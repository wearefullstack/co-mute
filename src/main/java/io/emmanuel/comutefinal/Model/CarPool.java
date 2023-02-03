package io.emmanuel.comutefinal.Model;

import jakarta.persistence.*;
import org.hibernate.annotations.JdbcTypeCode;
import org.hibernate.type.SqlTypes;

import java.time.LocalTime;
import java.util.Date;
/*
 * Add date field to car pool opportunity to know which day
 * the car pool opportunity is on will be auto generated and  not null
 */
@Entity
public class CarPool {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    @Column(name = "id", nullable = false)
    @JdbcTypeCode(SqlTypes.BIGINT)
    private int id;

    @GeneratedValue(strategy = GenerationType.AUTO)
    private Date date;

    private LocalTime depTime;
    private LocalTime arrTime;
    private String origin;
    private int numDayAvail;
    private String destination;
    private int numAvailSeats;
    @ManyToOne
    @JoinColumn(name = "user_userid")
    private UserModel user;

    public String getOwner() {
        return owner;
    }

    public void setOwner(String owner) {
        this.owner = owner;
    }

    private String owner;

    public void setDate(Date date) {
        this.date = date;
    }

    /*
    @ManyToOne
    @JoinColumn(name = "user_models_userid")
    private UserModel userModels;
    */
    private String notes;

    public CarPool( Date date, LocalTime depTime, LocalTime arrTime, String origin, int numDayAvail, String destination, int numAvailSeats, UserModel userModels, String notes) {

        this.date = date;
        this.depTime = depTime;
        this.arrTime = arrTime;
        this.origin = origin;
        this.numDayAvail = numDayAvail;
        this.destination = destination;
        this.numAvailSeats = numAvailSeats;
        this.notes = notes;
    }

    public CarPool( Date date, LocalTime depTime, LocalTime arrTime, String origin, int numDayAvail, String destination, int numAvailSeats, UserModel userModels) {
        this.date = date;
        this.depTime = depTime;
        this.arrTime = arrTime;
        this.origin = origin;
        this.numDayAvail = numDayAvail;
        this.destination = destination;
        this.numAvailSeats = numAvailSeats;

    }

    public Date getDate() {
        return date;
    }

    public CarPool() {
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public LocalTime getDepTime() {
        return depTime;
    }

    public void setDepTime(LocalTime depTime) {
        this.depTime = depTime;
    }

    public LocalTime getArrTime() {
        return arrTime;
    }

    public void setArrTime(LocalTime arrTime) {
        this.arrTime = arrTime;
    }

    public String getOrigin() {
        return origin;
    }

    public void setOrigin(String origin) {
        this.origin = origin;
    }

    public int getNumDayAvail() {
        return numDayAvail;
    }

    public void setNumDayAvail(int numDayAvail) {
        this.numDayAvail = numDayAvail;
    }

    public String getDestination() {
        return destination;
    }

    public void setDestination(String destination) {
        this.destination = destination;
    }

    public int getNumAvailSeats() {
        return numAvailSeats;
    }

    public void setNumAvailSeats(int numAvailSeats) {
        this.numAvailSeats = numAvailSeats;
    }

    public String getNotes() {
        return notes;
    }

    public void setNotes(String notes) {
        this.notes = notes;
    }
}
