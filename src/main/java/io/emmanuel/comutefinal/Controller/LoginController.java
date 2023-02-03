package io.emmanuel.comutefinal.Controller;

import io.emmanuel.comutefinal.Repository.UserRepository;
import io.emmanuel.comutefinal.Service.LoginService;
import io.emmanuel.comutefinal.Service.UserService;
import io.emmanuel.comutefinal.dto.LoginRequest;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.servlet.ModelAndView;

@Controller

public class LoginController {

    @Autowired
    UserService userService;
    @Autowired
    LoginService loginService;

    //Handler method used to return login view
    @RequestMapping(value = "/",method = RequestMethod.GET)
    public String showLoginPage(Model model){
        model.addAttribute("loginRequest",new LoginRequest());
        return "login";
    }

    //Handler method to authenticate login credentials
    @RequestMapping(value = "/", method = RequestMethod.POST)
    public ModelAndView validateLogin(Model model, @ModelAttribute("loginRequest") LoginRequest loginInfo, BindingResult result) {
        ModelAndView mav = new ModelAndView();
        if (result.hasErrors()) {
            model.addAttribute("errorMsg", "Oops! An Error has occured!");  //Handle error
            mav.setViewName("login");
        } else {
            boolean isValid = loginService.authenticateLogin(loginInfo.getEmail(), loginInfo.getPassword()); //Returns TRUE if User Credentials have been successfully authenticated
            if (isValid) {  //if user has been auth they view is set to Home and an userModel active User passed
                mav.setViewName("redirect:Home/" + userService.getUserId(loginInfo.getEmail(), loginInfo.getPassword()));
            } else { //View is set to login(same page) and the error message displayed
                model.addAttribute("errorMsg", "Invalid,Credentials, Please try again");
                mav.setViewName("login");
            }
        }
        return mav;
    }

}
