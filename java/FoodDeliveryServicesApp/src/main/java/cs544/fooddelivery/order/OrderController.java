package cs544.fooddelivery.order;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;

import cs544.fooddelivery.repositories.OrderDAO;

@Controller
public class OrderController {
	
	@Autowired
	OrderService orderService;
	
	@RequestMapping("/order/{orderId}")
	public String orderDetail(@PathVariable Long orderId,ModelMap model){
		model.addAttribute("order", this.orderService.getOrder(orderId));
		return "orderDetail";
	}
}
