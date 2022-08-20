//package com.comuteme.domain;
//
//import javax.persistence.*;
//import java.util.List;
//
//@Entity
//
//public class Owner {
//
//        @Id
//        @GeneratedValue(strategy=GenerationType.AUTO)
//        private long ownerid;
//
//        private String firstname, lastname;
//
//        @OneToMany(cascade = CascadeType.ALL, mappedBy = "owner")
//        private List<Carpool> carpools;
//
//
//        public Owner() {}
//
//        public Owner(String firstname, String lastname) {
//                super();
//                this.firstname = firstname;
//                this.lastname = lastname;
//        }
//
//        public long getOwnerid() {
//                return ownerid;
//        }
//        public void setOwnerid(long ownerid) {
//                this.ownerid = ownerid;
//        }
//        public String getFirstname() {
//                return firstname;
//        }
//        public void setFirstname(String firstname) {
//                this.firstname = firstname;
//        }
//        public String getLastname() {
//                return lastname;
//        }
//        public void setLastname(String lastname) {
//                this.lastname = lastname;
//
//        }
//
//        public List<Carpool> getCarpools() {
//                return carpools;
//        }
//
//        public void setCarpools(List<Carpool> carpools) {
//                this.carpools = carpools;
//        }
//}