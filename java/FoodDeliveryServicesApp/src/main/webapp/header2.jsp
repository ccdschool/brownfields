<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib uri="http://www.springframework.org/security/tags" prefix="sec" %>
<div>
	<h2>Food Delivery System</h2>
</div>
<div>
	<%@ include file="menu2.jsp" %>
	<span style="float:right">
		<c:choose>
			<c:when test="${sessionScope.user != null}">
				${sessionScope.user.userName} 
					| <a href="<c:url value='/myaccount' />">My Account</a>
				
				 | <a href="<c:url value='/logout' />" >Logout</a>
			</c:when>
			<c:otherwise>
				<a href="<c:url value='/login'/>">Login</a>
			</c:otherwise>
		</c:choose>
	</span>
</div>
<div><hr /></div>
