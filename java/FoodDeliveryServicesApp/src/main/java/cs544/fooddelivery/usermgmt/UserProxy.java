package cs544.fooddelivery.usermgmt;

import cs544.fooddelivery.domain.Customer;
import cs544.fooddelivery.domain.Supplier;
import cs544.fooddelivery.domain.User;

public class UserProxy extends User {
	private double deliveryRadius;
	private String userType;
	
	private static final String USER_TYPE_SUPPLIER = "supplier";
	private static final String USER_TYPE_CUSTOMER = "customer";
	
	public UserProxy(){}
	
	public UserProxy(Supplier supplier){
		super(supplier.getUserName(), supplier.getPassword(), supplier.getFullName(), supplier.getAddress(), supplier.getEmail(), supplier.getContact(), supplier.isActive());
		setId(supplier.getId());
		userType = USER_TYPE_SUPPLIER;
		deliveryRadius = supplier.getDeliveryRadius();
	}
	
	public UserProxy(Customer customer){
		super(customer.getUserName(), customer.getPassword(), customer.getFullName(), customer.getAddress(), customer.getEmail(), customer.getContact(), customer.isActive());
		setId(customer.getId());
		userType = USER_TYPE_CUSTOMER;
	}
	
	public User getDomainUser(){
		User user = null;
		System.out.println(getId());
		if(userType.equals(USER_TYPE_SUPPLIER)){
			user = new Supplier(getUserName(), getPassword(), getFullName(), getAddress(), getEmail(), getContact(), true, deliveryRadius);
		}else{
			user = new Customer(getUserName(), getPassword(), getFullName(), getAddress(), getEmail(), getContact(), true);
		}
		user.setId(getId());
		return user;
	}

	public double getDeliveryRadius() {
		return deliveryRadius;
	}

	public void setDeliveryRadius(double deliveryRadius) {
		this.deliveryRadius = deliveryRadius;
	}

	public String getUserType() {
		return userType;
	}

	public void setUserType(String userType) {
		this.userType = userType;
	}
}
