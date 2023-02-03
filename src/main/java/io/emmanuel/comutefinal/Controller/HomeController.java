package io.emmanuel.comutefinal.Controller;



import io.emmanuel.comutefinal.Model.CarPool;
import io.emmanuel.comutefinal.Service.CarpoolService;
import io.emmanuel.comutefinal.Service.UserService;
import io.emmanuel.comutefinal.dto.CarpoolRequest;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@Controller
public class HomeController {

  @Autowired
    CarpoolService carpoolService;
  @Autowired
    UserService userService;

    @RequestMapping(value = "/Home/{theUserId}", method = RequestMethod.GET)
    public String showHomePage(@PathVariable("theUserId") int myId, Model model){

        //Add list of car pool joined

        List<CarPool> listCarPool = carpoolService.listAllCarPool();

        if(listCarPool.isEmpty()) {
            model.addAttribute("errorMsg", "You currently Do not have any joined Car Pool Opportunities");
        }else {
            model.addAttribute("listCarPool", listCarPool);
            model.addAttribute("theUserId",myId);
        }
        return "Home";
    }
    @RequestMapping(value = "/Home/Createcarpool/{uid}",method = RequestMethod.GET)
    public String showCarPool(Model model, @PathVariable("uid") int myId){
        model.addAttribute("CarpoolRequest",new CarpoolRequest());
        model.addAttribute("theUserId",myId);
        model.addAttribute("owner",userService.getUserName(myId));
        return "Createcarpool";
    }


    @RequestMapping(value = "/Home/Createcarpool/{uid}",method = RequestMethod.POST)
    public String CreateCarpool(Model model,@PathVariable(value="uid") int id, @ModelAttribute("CarpoolRequest") CarpoolRequest carPoolInfo, BindingResult result){
        if(result.hasErrors()){ //BindingResult --  error Message
            model.addAttribute("Message","Oops -- Technical error has occured" );
        }else {//Success Message
            model.addAttribute("Message",carpoolService.setRepository(carPoolInfo,id));
        }
        return "Createcarpool";
    }

}
