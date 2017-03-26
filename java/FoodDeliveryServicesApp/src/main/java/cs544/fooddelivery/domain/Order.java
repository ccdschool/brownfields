package cs544.fooddelivery.domain;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.EnumType;
import javax.persistence.Enumerated;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.ManyToOne;
import javax.persistence.OneToMany;
import javax.persistence.Transient;

@Entity(name = "OrderTable")
public class Order {
	@Id
	@GeneratedValue
	private Long id;

	private Date orderDate;
	@Transient
	private Long supplierId;

	@OneToMany(mappedBy = "order", fetch = FetchType.EAGER,cascade=CascadeType.ALL)
	private List<OrderLine> orderLines;

	@ManyToOne
	private Customer customer;

	@ManyToOne
	private Delivery delivery;

	public Order() {
		orderLines = new ArrayList<OrderLine>();
	}

	public Long getId() {
		return id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public Date getOrderDate() {
		return orderDate;
	}

	public void setOrderDate(Date orderDate) {
		this.orderDate = orderDate;
	}

	public List<OrderLine> getOrderLines() {
		return orderLines;
	}

	public void setOrderLines(List<OrderLine> orderLines) {
		this.orderLines = orderLines;
	}

	public Customer getCustomer() {
		return customer;
	}

	public void setCustomer(Customer customer) {
		this.customer = customer;
	}

	public Delivery getDelivery() {
		return delivery;
	}

	public void setDelivery(Delivery delivery) {
		this.delivery = delivery;
	}	

	// methods
	public double getTotalPrice() {
		
		double totalPrice=0.0;
		for (OrderLine ol : this.orderLines) {
			totalPrice += ol.getTotalPrice();
		}

		return totalPrice;
	}

	public Long getSupplierId() {
		return supplierId;
	}

	public void setSupplierId(Long supplierId) {
		this.supplierId = supplierId;
	}
	
	public void addOrderLine(OrderLine ol){
		orderLines.add(ol);
		ol.setOrder(this);
	}
	
}