package cs544.fooddelivery.log;

import org.apache.log4j.Logger;
import org.springframework.stereotype.Component;

@Component
public class LogWriter {
	private Logger logger = Logger.getLogger("");
	
	public void writeInfoLog(String msg){
		logger.info(msg);
	}
	
	public void writeErrorLog(String msg){
		logger.error(msg);
	}
	
	public void writeDebugLog(String msg){
		logger.debug(msg);
	}
}
