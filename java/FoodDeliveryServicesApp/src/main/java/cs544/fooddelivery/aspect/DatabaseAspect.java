package cs544.fooddelivery.aspect;

import java.io.IOException;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpSession;

import org.aspectj.lang.JoinPoint;
import org.aspectj.lang.ProceedingJoinPoint;
import org.aspectj.lang.annotation.After;
import org.aspectj.lang.annotation.Around;
import org.aspectj.lang.annotation.Aspect;
import org.aspectj.lang.annotation.Before;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.ui.ModelMap;
import org.springframework.util.StopWatch;
import org.springframework.web.bind.annotation.PathVariable;

import cs544.fooddelivery.domain.Order;
import cs544.fooddelivery.domain.OrderLine;
import cs544.fooddelivery.domain.User;
import cs544.fooddelivery.emailSender.EmailService;
import cs544.fooddelivery.log.LogWriter;

@Aspect
public class DatabaseAspect {
	
	@Autowired
	private LogWriter logWriter;
	
	@Autowired
	private EmailService emailService;
	
	@Around("execution(* cs544.fooddelivery.repositories..*.*(..))")
	public Object logTime(ProceedingJoinPoint joinPoint) throws Throwable {
		StopWatch sw = new StopWatch();
		sw.start();
		Object obj = joinPoint.proceed();
		sw.stop();
		String message = "Function call: " + joinPoint.getTarget().getClass().getName() + "." 
				+ joinPoint.getSignature().getName() + " took " + " " + sw.getLastTaskTimeMillis() + "ms";
		logWriter.writeInfoLog(message);
		return obj;
	}
	
	@Before("execution(* cs544.fooddelivery.supplier.SupplierService.*(..))")
	public void beforeAnyMethodSupplier(JoinPoint jp){
		System.out.println("before supplier controller method executed:"+jp.getSignature());
	}
	
	@After("execution(* cs544.fooddelivery.customer.CustomerController.placeOrder(..))")
	public void afterPlaceaorder(JoinPoint jp) throws IOException{
		User user = (User) jp.getArgs()[0];
		HttpServletRequest request = (HttpServletRequest) jp.getArgs()[1];
		HttpSession session = request.getSession();
		
		Order cart = (Order) session.getAttribute("order");
		String msg="Dear "+user.getFullName()+", <br>";
		msg+="<ul>";
		for(OrderLine o:cart.getOrderLines()){
			msg+="<li>"+o.getFoodItem().getName()+" $"+o.getFoodItem().getPrice()+" "+o.getQuantity()+"</li>";
		}
		msg+="</ul>";
		msg+="Total Price : $"+cart.getTotalPrice();
		
		emailService.sendMail("support@fooddelivery.com", user.getEmail(), "Your New Orders", msg);
		String message = "Email send to "+user.getFullName()+" "+user.getEmail();
		logWriter.writeInfoLog(message);
		//System.out.println();
	}
}
