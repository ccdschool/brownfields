package cs544.fooddelivery.repositories;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import cs544.fooddelivery.domain.Delivery;

@Repository
@Transactional(propagation=Propagation.MANDATORY)
public interface DeliveryDAO extends JpaRepository<Delivery, Long>{
	@Query("SELECT DISTINCT d FROM Delivery d JOIN d.orders o JOIN o.orderLines ol WHERE ol.foodItem.supplier.id= ?1")
	public List<Delivery> findAllBySupplierId(long supplierId);
	
	@Query("SELECT DISTINCT d FROM Delivery d WHERE id = ?1")
	public Delivery getDelivery(long id);
}
