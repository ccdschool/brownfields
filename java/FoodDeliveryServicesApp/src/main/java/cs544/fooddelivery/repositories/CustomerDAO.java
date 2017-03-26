package cs544.fooddelivery.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import cs544.fooddelivery.domain.Customer;


@Repository("customerRepository")
public interface CustomerDAO extends JpaRepository<Customer, Long>{

}
