<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
 <%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
    
<table>
	<tr>
		<td><a href="<c:url value='/myprofile' />">My profile</a><br /> <a
			href="<c:url value='/myorder' />">Orders</a><br /> <a
			href="<c:url value='/orderhistory' />">Order History</a><br /></td>
		<td>
			
				<%@ include file="signup.jsp" %>
		</td>

	</tr>

</table>