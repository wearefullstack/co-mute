package io.emmanuel.comutefinal.Controller;

import io.emmanuel.comutefinal.Service.RegisterService;
import io.emmanuel.comutefinal.Service.UserService;
import io.emmanuel.comutefinal.dto.RegisterRequest;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.ui.ModelMap;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.servlet.ModelAndView;

@Controller
public class RegisterController {
    @Autowired
    RegisterService registerService;

    @Autowired
    UserService userService;

    @RequestMapping(value = "/Register", method = RequestMethod.GET)
    public String showRegisterPage(Model model){

        model.addAttribute("RegisterRequest",new RegisterRequest() );
        return "Register";
    }

    @RequestMapping(value = "/Register", method = RequestMethod.POST)
    public ModelAndView addUser(Model model, @ModelAttribute("RegisterRequest") RegisterRequest userInfo, BindingResult result){
        ModelAndView mavReg = new ModelAndView();
        if(result.hasErrors()){ //BindingResult
            model.addAttribute("errorMsg","Oops, Technical Error has occured" );
            mavReg.setViewName("Register");
        }else {
            boolean isSuccess = registerService.registerUser(userInfo.getName(),userInfo.getSurname(),userInfo.getPhone(),userInfo.getEmail() ,userInfo.getPassword());
            if (isSuccess) {  //if user has been successfully created the view is set to Home and an userModel active User passed
                mavReg.setViewName("redirect:Home/" + userService.getUserId(userInfo.getEmail(), userInfo.getPassword()));
            }else { //View is set to Register(same page) and the error message displayed
                    model.addAttribute("errorMsg", "Oops Unsuccessful registration, Please try again");
                mavReg.setViewName("Register");
                }
        }
        return mavReg;
    }

}
