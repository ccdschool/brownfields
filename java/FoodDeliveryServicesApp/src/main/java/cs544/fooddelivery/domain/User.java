package cs544.fooddelivery.domain;

import javax.persistence.DiscriminatorColumn;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.Inheritance;
import javax.persistence.InheritanceType;
import javax.persistence.Transient;
import javax.validation.constraints.Size;

import org.hibernate.validator.constraints.Email;
import org.hibernate.validator.constraints.NotEmpty;

@Entity
@Inheritance(strategy=InheritanceType.SINGLE_TABLE)
@DiscriminatorColumn(name="userRole")
public abstract class User {
	@Id
	@GeneratedValue
	private long id;
	
	@Size(min=6,message="Username should be at least 6 characters long")
	private String userName;
	
	@Size(min=6,message="Password should be at least 6 characters long")
	private String password;
	
	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	@Size(min=1,message="Cannot be empty")
	private String fullName;
	
	@Size(min=1,message="Cannot be empty")
	private String address;
	
	@NotEmpty
	@Email(message="Invalid email")
	private String email;
	
	@Size(min=1,message="Cannot be empty")
	private String contact;
	
	private boolean isActive;
	
	public User(){
		isActive = true;
	}
	
	public User(String userName, String password, String fullName, String address, String email, String contact,
			boolean isActive) {
		this.userName = userName;
		this.password = password;
		this.fullName = fullName;
		this.address = address;
		this.email = email;
		this.contact = contact;
		this.isActive = true;
	}

	public String getUserName() {
		return userName;
	}
	
	public void setUserName(String userName) {
		this.userName = userName;
	}
	
	public String getPassword() {
		return password;
	}
	
	public void setPassword(String password) {
		this.password = password;
	}
	
	public String getFullName() {
		return fullName;
	}
	
	public void setFullName(String fullName) {
		this.fullName = fullName;
	}
	
	public String getAddress() {
		return address;
	}
	
	public void setAddress(String address) {
		this.address = address;
	}
	
	public String getEmail() {
		return email;
	}
	
	public void setEmail(String email) {
		this.email = email;
	}
	
	public String getContact() {
		return contact;
	}
	
	public void setContact(String contact) {
		this.contact = contact;
	}
	
	public boolean isActive() {
		return isActive;
	}
	
	public void setActive(boolean isActive) {
		this.isActive = isActive;
	}
}