package cs544.fooddelivery.usermgmt;

import java.util.Set;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.authority.AuthorityUtils;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.web.authentication.logout.SecurityContextLogoutHandler;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.servlet.mvc.support.RedirectAttributes;

import cs544.fooddelivery.domain.Admin;
import cs544.fooddelivery.domain.Customer;
import cs544.fooddelivery.domain.Supplier;
import cs544.fooddelivery.domain.User;
import cs544.fooddelivery.log.LogWriter;

@Controller
public class UserMgmtController {
	
	@Autowired
	private UserMgmtService userMgmtService;
	
	@Autowired
	private LogWriter logWriter;
	
	@RequestMapping("/login")
	public String login(
		@RequestParam(value = "error", required = false) String error,
		@RequestParam(value = "logout", required = false) String logout, Model model) {

		if (error != null) {
			model.addAttribute("error", "Invalid username and password!");
			logWriter.writeInfoLog("Login not successful");
		}

		if (logout != null) {
			model.addAttribute("msg", "You've been logged out successfully.");
			logWriter.writeInfoLog("Login successful");
		}

		return "login";
	}
	
	@RequestMapping(value={"/loginSuccess", "/"})
	public String loginSuccess(HttpServletRequest request){
		String userName = SecurityContextHolder.getContext().getAuthentication().getName();
		Set<String> roles = AuthorityUtils.authorityListToSet(SecurityContextHolder.getContext().getAuthentication().getAuthorities());
		HttpSession session = request.getSession();
		if(roles.contains("ROLE_ADMIN")){
			User user = new Admin();
			session.setAttribute("user", user);
			//return new RedirectView("dashboard_admin");
			return "redirect:/dashboard_admin";
		}else if(roles.contains("ROLE_SUPPLIER")){
			userMgmtService.setLoggedInUser(userName);
			session.setAttribute("user", userMgmtService.getLoggedInUser());
			return "redirect:/supplier";
			//return new RedirectView("supplier");
		}else{
			userMgmtService.setLoggedInUser(userName);
			session.setAttribute("user", userMgmtService.getLoggedInUser());
			//return new RedirectView("home");
			return "redirect:/home";
		}
	}
	
	@RequestMapping("/signup")
	public String openSignup(Model model){
		model.addAttribute("user", new UserProxy());
		return "signup";
	}
	
	@RequestMapping(value="/signup", method=RequestMethod.POST)
	public String signup(@ModelAttribute("user") @Validated UserProxy user, BindingResult result, RedirectAttributes attrs){
		if(userMgmtService.getUserByUserName(user.getUserName()) != null){
			result.rejectValue("userName", "", "Username is already taken");
		}
		
		if(result.hasErrors()){
			return "signup";
		}else{
			User domainUser = user.getDomainUser();
			userMgmtService.addNewUser(domainUser);
			attrs.addFlashAttribute("msg", "Signup successful! You can now login");
			logWriter.writeInfoLog("Signup successful");
			return "redirect:login";
		}
	}
	
	@RequestMapping(value="/logout", method = RequestMethod.GET)
	public String logoutPage (HttpServletRequest request, HttpServletResponse response) {
	    Authentication auth = SecurityContextHolder.getContext().getAuthentication();
	    if (auth != null){    
	        new SecurityContextLogoutHandler().logout(request, response, auth);
	    }
	    logWriter.writeInfoLog("Login out successful");
	    return "redirect:/login?logout";
	}
	
	@RequestMapping(value="/user/update")
	public String openUserUpdate(Model model){
		User user = userMgmtService.getLoggedInUser();
		model.addAttribute("isEdit", true);
		if(user instanceof Supplier){
			model.addAttribute("user", new UserProxy((Supplier) user));
		}else{
			model.addAttribute("user", new UserProxy((Customer) user));
		}
		return "editProfile";
	}
	
	@RequestMapping(value="/user/update", method=RequestMethod.POST)
	public String updateUser(@ModelAttribute("user") @Validated UserProxy user, BindingResult result, HttpServletRequest request){
		if(result.hasErrors()){
			return "signup";
		}else{
			User domainUser = user.getDomainUser();
			userMgmtService.addNewUser(domainUser);
			userMgmtService.setLoggedInUser(domainUser.getUserName());
			request.getSession().setAttribute("user", domainUser);
			return "redirect:/home";
		}
	}
}
