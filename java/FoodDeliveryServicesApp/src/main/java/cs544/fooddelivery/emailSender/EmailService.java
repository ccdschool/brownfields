package cs544.fooddelivery.emailSender;

import java.io.IOException;

import javax.annotation.Resource;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.mail.MailSender;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.stereotype.Service;

import com.sendgrid.Content;
import com.sendgrid.Email;
import com.sendgrid.Mail;
import com.sendgrid.Method;
import com.sendgrid.Request;
import com.sendgrid.Response;
import com.sendgrid.SendGrid;

@Service
public class EmailService {
	
	public static final String SENDGRID_API_KEY ="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";
	
	public void sendMail(String from, String to, String subject, String msg) throws IOException {

		Email from1 = new Email(from);
	    Email to1 = new Email(to);
	    Content content = new Content("text/html", msg);
	    Mail mail = new Mail(from1, subject, to1, content);
	    // you can set template Id if you create template in sendGrid account
	   // mail.setTemplateId("c2fdca09-bc06-416c-bb0b-31b5c6a9feb2");
	   // mail.setSubject("Hello Prasanna Subject");
	    
	    /*
	     * set API KEY of your SendGrid A/c
	     * SendGrid sg = new SendGrid("API_KEY");
	     */
	    SendGrid sg = new SendGrid(SENDGRID_API_KEY);
	    Request request = new Request();
	    try {
	      request.method = Method.POST;
	      request.endpoint = "mail/send";
	      request.body = mail.build();
	      Response response = sg.api(request);
	      System.out.println(response.statusCode);
	      System.out.println(response.body);
	      System.out.println(response.headers);
	    } catch (IOException ex) {
	      throw ex;
	    }
	}

}
