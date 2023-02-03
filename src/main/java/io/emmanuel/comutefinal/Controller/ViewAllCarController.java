package io.emmanuel.comutefinal.Controller;

import io.emmanuel.comutefinal.Model.CarPool;
import io.emmanuel.comutefinal.Service.CarpoolService;
import io.emmanuel.comutefinal.Service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import java.util.List;

@Controller
public class ViewAllCarController {
    @Autowired
    CarpoolService carpoolService;
    @Autowired
    UserService userService;

    @RequestMapping(value = "/Home/ViewAllCarpool/{theUserId}", method = RequestMethod.GET)
    public String showViewAllPage(@PathVariable("theUserId") int myId, Model model){

        List<CarPool> listCarPool = carpoolService.listAllCarPool();

        if(listCarPool.isEmpty()) {
            model.addAttribute("errorMsg", "There are currently no Car Pool Opportunities");
        }else {
            model.addAttribute("listCarPool", listCarPool);
            model.addAttribute("theUserId",myId);
        }
        return "ViewAllCarPool";
    }
}
