package io.emmanuel.comutefinal.dto;

import java.time.LocalTime;

public class CarpoolRequest {
    public LocalTime depTime;
    public LocalTime arrTime;
    public String origin;
    public int numDayAvail;
    public String destination;
    public int numAvailSeats;
    public String owner;
    public String notes;

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

    public String getOwner() {
        return owner;
    }

    public void setOwner(String owner) {
        this.owner = owner;
    }

    public String getNotes() {
        return notes;
    }

    public void setNotes(String notes) {
        this.notes = notes;
    }
}
