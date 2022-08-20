//package com.comuteme.securityconfig;
//
//import io.jsonwebtoken.JwtBuilder;
//import io.jsonwebtoken.Jwts;
//import io.jsonwebtoken.SignatureAlgorithm;
//import org.springframework.stereotype.Component;
//import io.jsonwebtoken.security.Keys;
//import java.security.Key;
//import java.util.Date;
//
//@Component
//public class JwtService {
//
//    static final long EXPIRATIONTIME = 86400000; // 1day
//    static final String PREFIX = "Bearer"; // Generate key only for demo remember to read it from the "application" itself.
//    static final Key key = Key.secrectKeyFor(SignatureAlgorithm.HS256);
//    public String getTowken(String firstname){
//        String token = Jwts.builder()
//                .setSubject(firstname)
//                .setExpiration(new Date(System.currentTimeMillis() + EXPIRATIONTIME))
//                .signWith(key)
//                .compact();
//
//        return token;
//    }
//}
