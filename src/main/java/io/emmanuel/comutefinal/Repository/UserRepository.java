package io.emmanuel.comutefinal.Repository;


import io.emmanuel.comutefinal.Model.UserModel;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.transaction.annotation.Transactional;

public interface UserRepository extends JpaRepository<UserModel, Long> {
    /*

    @Transactional
    @Modifying
    @Query("update UserModel u set u.password = ?1 where u.userid = ?2")
    int updatePasswordByUserid(String password, int userid);

    @Transactional
    @Modifying
    @Query("update UserModel u set u.phone = ?1 where u.userid = ?2")
    int updatePhoneByUserid(String phone, int userid);

    @Transactional
    @Modifying
    @Query("update UserModel u set u.email = ?1 where u.userid = ?2")
    int updateEmailByUserid(String email, int userid);

    @Transactional
    @Modifying
    @Query("update UserModel u set u.name = ?1 where u.userid = ?2")
    int updateNameByUserid(String name, int userid);

    @Transactional
    @Modifying
    @Query("update UserModel u set u.name = ?1 where u.name = ?2")
    void updateNameByName(String name);

     */
    @Query("select u from UserModel u where u.userid = ?1")
    UserModel findByUserid(int userid);
    @Query("SELECT u FROM UserModel u where upper(u.email) like upper(?1) and upper(u.password) like upper(?2)")
    UserModel findByEmailAndPassword(String email, String password);


}