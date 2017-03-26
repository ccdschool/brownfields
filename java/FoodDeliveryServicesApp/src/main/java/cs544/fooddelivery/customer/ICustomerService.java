package cs544.fooddelivery.customer;

import java.util.List;

import javax.servlet.http.HttpSession;

import cs544.fooddelivery.domain.FoodItem;
import cs544.fooddelivery.domain.Order;
import cs544.fooddelivery.domain.OrderLine;

public interface ICustomerService {
	
	List<Order> orders();
	List<Order> orderHistory();
	Order addToCart(FoodItem item, HttpSession request);
	Order updateCart(OrderLine orderLine, HttpSession request);
	Order removeFromOrder(OrderLine orderLine, HttpSession request);

}
