package cs544.fooddelivery.domain;

import java.util.List;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import javax.persistence.OneToMany;

@Entity
@DiscriminatorValue("ROLE_CONSUMER")
public class Customer extends User {
	@OneToMany(mappedBy="customer")
	private List<Order> orders;
	
	public Customer(){
		super();
	}
	
	public Customer(String userName, String password, String fullName, String address, String email, String contact,
			boolean isActive) {
		super(userName, password, fullName, address, email, contact, isActive);
	}
}
