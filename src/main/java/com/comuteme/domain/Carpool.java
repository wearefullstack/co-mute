package com.comuteme.domain;


// intention here is to create a vehicle object that will be "owned" by a user and used in our carpool
/*
 * The entity class fields are mapped to database table columns.
 * The entity class must also contain a unique ID that is used as a primary key in the database
 *
 *
 * NOTE:
 * might need to explore
 *  With the @Column annotation, you can define the column's length and whether the column is nullable.
 * The following code shows an example of using the @Column annotation. With this definition,
 * @Column(name="explanation", nullable=false, length=512)
 * private String description
 * the column's name in the database is explanation, the length of the column is 512, and it is not nullable.

 * A user can create any number of car-pool opportunities, however they cannot register two car-pool
 * opportunities with overlapping time-frames.
 * The user that formed/created the opportunity will be the owner/leader
 * Optional requirement : days per-week? use array len [7]?
 * */


import javax.persistence.*;
import java.util.Collection;
import java.util.Date;
import java.util.HashSet;

@Entity
public class Carpool {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private long id;
    private String origin;
    private String departureTime;
    private String destination;
    private String expectedArrivalTime;
    private int availableSeats;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "owner")
    private CoMuteUser owner;

    @ManyToMany(fetch = FetchType.LAZY)
    @JoinColumn(name = "coMuteUsers")
    private Collection<CoMuteUser> coMuteUsers = new HashSet<>();


    public Carpool() {
    }

    public Carpool(String origin, String departureTime, String destination, String expectedArrivalTime, int availableSeats) {
        super();
        this.origin = origin;
        this.departureTime = departureTime;
        this.destination = destination;
        this.expectedArrivalTime = expectedArrivalTime;
        this.availableSeats = availableSeats;
    }

    public Carpool(String origin, String departureTime, String destination, String expectedArrivalTime, int availableSeats, CoMuteUser owner) {
        super();
        this.origin = origin;
        this.departureTime = departureTime;
        this.destination = destination;
        this.expectedArrivalTime = expectedArrivalTime;
        this.availableSeats = availableSeats;
        this.owner = owner;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public String getOrigin() {
        return origin;
    }

    public void setOrigin(String origin) {
        this.origin = origin;
    }

    public String getDepartureTime() {
        return departureTime;
    }

    public void setDepartureTime(String departureTime) {
        this.departureTime = departureTime;
    }

    public String getDestination() {
        return destination;
    }

    public void setDestination(String destination) {
        this.destination = destination;
    }

    public String getExpectedArrivalTime() {
        return expectedArrivalTime;
    }

    public void setExpectedArrivalTime(String expectedArrivalTime) {
        this.expectedArrivalTime = expectedArrivalTime;
    }

    public int getAvailableSeats() {
        return availableSeats;
    }

    public void setAvailableSeats(int availableSeats) {
        this.availableSeats = availableSeats;
    }

    public CoMuteUser getOwner() {
        return owner;
    }

    public void setOwner(CoMuteUser owner) {
        this.owner = owner;
    }

    public Collection<CoMuteUser> getCoMuteUsers() {
        return coMuteUsers;
    }

    public void setCoMuteUsers(Collection<CoMuteUser> coMuteUsers) {
        this.coMuteUsers = coMuteUsers;
    }

//    public User getUser() {
//        return user;
//    }
//
//    public void setUser(User user) {
//        this.user = user;
//    }
}
