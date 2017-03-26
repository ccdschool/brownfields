package cs544.fooddelivery.admin;

import java.util.ArrayList;
import java.util.Date;

import javax.servlet.http.HttpServletRequest;
import javax.validation.Valid;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.ui.ModelMap;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.servlet.mvc.support.RedirectAttributes;

import cs544.fooddelivery.domain.Category;
import cs544.fooddelivery.domain.Delivery;
import cs544.fooddelivery.domain.FoodItem;
import cs544.fooddelivery.domain.Status;
import cs544.fooddelivery.domain.Supplier;
import cs544.fooddelivery.domain.User;
import cs544.fooddelivery.order.OrderService;
import cs544.fooddelivery.usermgmt.UserMgmtService;

@Controller
public class AdminController {
	
	@Autowired
	AdminService adminService;

	@RequestMapping("/dashboard_admin")
	public String displayAdminDashboard(ModelMap model){
	//	long supplierId = userService.getLoggedInUser().getId();
		model.addAttribute("categories", adminService.getAllCategories());
		return "dashboard_admin";
	}
	
	@RequestMapping(value="/addCategoryPage", method=RequestMethod.GET)	
    public String addCategoryPage( Model model)
	{
        return "addCategory";
    }
	
	@RequestMapping(value="/addCategory" , method=RequestMethod.POST)
	public String categoryAdd(@Valid @ModelAttribute("categoryAdd") Category category,@RequestParam String categoryName,BindingResult bindingResult, Model model, 
    		RedirectAttributes redirectAttributes, HttpServletRequest request){
		Category categoryObj = new Category();
		categoryObj.setName(categoryName);
		this.adminService.save(categoryObj);
		return "redirect:/dashboard_admin";
	}
	

	@RequestMapping(value="/editCategoryPage/{id}", method=RequestMethod.GET)	
    public String editCategoryPage( @PathVariable("id")int id, Model model)
	{
		model.addAttribute("oldCategory",this.adminService.getCategory(id));
        return "editCategory";
    }
	

	@RequestMapping(value = "/updateCategory/{id}", method = RequestMethod.POST)
	public String updateCategory( @PathVariable("id")int id, @RequestParam(value="categoryName") String name) {
	
		this.adminService.EditCategory(id, name);
		
	//	return "dashboard_admin";
		return "redirect:/dashboard_admin";
	}
	
	@RequestMapping(value = "/deleteCategory/{id}", method = RequestMethod.GET)
	public String deleteCategory( @PathVariable("id")int id,Model model) {
	
		adminService.delete(id);
		return "redirect:/dashboard_admin";
		
	}

	
}
