package cs544.fooddelivery.admin;

import java.util.Date;
import java.util.List;

//package cs544.fooddelivery.usermgmt;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import cs544.fooddelivery.domain.Category;
import cs544.fooddelivery.domain.Delivery;
import cs544.fooddelivery.domain.FoodItem;
import cs544.fooddelivery.domain.Order;
import cs544.fooddelivery.domain.Status;
import cs544.fooddelivery.order.OrderService;
import cs544.fooddelivery.repositories.CategoryDAO;
import cs544.fooddelivery.repositories.DeliveryDAO;
import cs544.fooddelivery.repositories.FoodItemDAO;

@Service
@Transactional(propagation=Propagation.REQUIRED)
public class AdminService {

	@Autowired
	private CategoryDAO categoryDAO;
	
	
	
	public List<Category> getAllCategories(){
		return this.categoryDAO.findAll();
	}
	
	
	public void save(Category category){
		this.categoryDAO.save(category);
		
	}
		
	
	public void delete(long id){
		this.categoryDAO.delete(id);
		
	}
	public Category getCategory(long id){
		return this.categoryDAO.findOne(id);
	}
	
	
	public void EditCategory(long deliveryId, String name) {
		Category category = this.categoryDAO.findOne(deliveryId);
		
		category.setName(name);
		this.categoryDAO.saveAndFlush(category);
	}

}

