package cs544.fooddelivery.supplier;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Date;
import java.util.List;
import java.util.UUID;

import javax.servlet.ServletContext;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.ui.ModelMap;
import org.springframework.validation.BindingResult;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.multipart.MultipartFile;

import cs544.fooddelivery.domain.Category;
import cs544.fooddelivery.domain.Delivery;
import cs544.fooddelivery.domain.FoodItem;
import cs544.fooddelivery.domain.Status;
import cs544.fooddelivery.domain.Supplier;
import cs544.fooddelivery.domain.User;
import cs544.fooddelivery.log.LogWriter;
import cs544.fooddelivery.order.OrderService;
import cs544.fooddelivery.usermgmt.UserMgmtService;

@Controller
public class SupplierController {
	
	@Autowired
	private ServletContext servletContext;
	
	public void setServletContext(ServletContext servletContext) {
		this.servletContext = servletContext;
	}

	@Autowired
	SupplierService supplierService;
	
	@Autowired
	UserMgmtService userService;
	
	@Autowired
	OrderService orderService;
	
	@Autowired
	private LogWriter logWriter;
	
	@RequestMapping("/supplier")
	public String displaySupplierDashboard(ModelMap model){
		long supplierId = userService.getLoggedInUser().getId();
		model.addAttribute("orders", orderService.getAllRequestedOrderForSupplierId(supplierId));
		return "supplier";
	}
	
	//	public @ResponseBody String uploadFileHandler( @RequestParam("file") MultipartFile file) {
	
//	public String signup(@ModelAttribute("user") @Validated UserProxy user, BindingResult result, RedirectAttributes attrs){
	
	@RequestMapping("/supplier/manageFoodItem/add")
	public String foodItemAdd(ModelMap model){
		model.addAttribute("categories", this.supplierService.getAllCategories());
		model.addAttribute("foodItem",new FoodItem());
		
//		logWriter.wr
		
		return "addFoodItem";
	}
	
	@RequestMapping(value="/supplier/manageFoodItem/add", method=RequestMethod.POST)
	public String addFoodItem(@ModelAttribute("foodItem") @Validated FoodItem fi, BindingResult br,ModelMap model){

		if(br.hasErrors()){
			model.addAttribute("categories", this.supplierService.getAllCategories());
//			model.addAttribute("foodItem",new FoodItem());
			return "addFoodItem";
		}
		
		if(fi.getFile().getSize() > 0) {
			fi.setImgUrl(this.saveImage(fi.getFile()));
    	}else{
    		fi.setImgUrl(fi.getImgUrl());
    	}

		fi.setCategory(this.supplierService.getCategoryWithCategoryId(fi.getCategoryId()));
		
		fi.setSupplier((Supplier)this.userService.getLoggedInUser());
		this.supplierService.saveFoodItem(fi);

		return "redirect:/supplier/manageFoodItem?";
	}
	
	private String saveImage(MultipartFile image)  {
		UUID idOne = UUID.randomUUID();
    	String fileName = idOne.toString();

    	String fullPathName = servletContext.getRealPath("/resources/images") + "/" + fileName;
	    File file = new File(fullPathName);
	    		
	    FileOutputStream stream;
		try {
			stream = new FileOutputStream(file);
		    stream.write(image.getBytes());
		    stream.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	    return "/resources/images/" + fileName;
	}

	
	
	@RequestMapping("/supplier/manageFoodItem/edit/{foodItemId}")
	public String foodITemEdit(ModelMap model,@PathVariable("foodItemId") Long foodItemId){
		model.addAttribute("categories", this.supplierService.getAllCategories());
		model.addAttribute("editItem", this.supplierService.getFoodItemForId(foodItemId));
		return "addFoodItem";
	}
	
	@RequestMapping("/supplier/manageFoodItem/delete/{foodItemId}")
	public String foodItemDelete(@PathVariable("foodItemId") Long foodItemId){
		this.supplierService.deleteFoodItemForId(foodItemId);
		return "redirect:/supplier/manageFoodItem";
	}
	
	@RequestMapping("/supplier/manageFoodItem")
	public String manageFoodItem(ModelMap model){
		model.addAttribute("foodItems", this.supplierService.getAllFoodItemBySupplier_Id(userService.getLoggedInUser().getId()));
		return "manageFoodItem";
	}
	
	@RequestMapping(value="/supplier/makeDelivery", method=RequestMethod.POST)
	public String makeDelivery(@RequestParam(value="orderIds[]") String[] orderIds){
		supplierService.saveDelivery(new Date(), orderIds);
		logWriter.writeInfoLog("Delivery scheduled by supplier: " + userService.getLoggedInUser().getUserName());
		return "redirect:/supplier";
	}
	
	@RequestMapping(value="/supplier/deliveries")
	public String deliveryList(Model model){
		User loggedInUser = userService.getLoggedInUser();
		model.addAttribute("deliveries", supplierService.getAllDeliveries(loggedInUser.getId()));
		return "deliverylist";
	}
	
	@RequestMapping(value="/supplier/deliveries/{deliveryId}")
	public String deliveryDetail(@PathVariable long deliveryId, Model model){
		Delivery delivery = supplierService.getDelivery(deliveryId);
		model.addAttribute("delivery", delivery);
		return "deliverydetail";
	}
	
	@RequestMapping(value="/supplier/deliveries/{deliveryId}", method=RequestMethod.POST)
	public String deliveryDetailUpdate(@PathVariable long deliveryId, @RequestParam(value="distance") int distance){
		supplierService.completeDelivery(deliveryId, new Date(), distance);
		logWriter.writeInfoLog("Delivery completed by supplier " + userService.getLoggedInUser().getUserName());
		return "redirect:/supplier/deliveries";
	}
}
