<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt" %>
<link rel="stylesheet" href="<c:url value="/resources/css/index.css" />">

<h2>List of deliveries</h2>

<table border=1>
	<tr>
		<th>Start Date</td>
		
		<th>Delivery date</td>
		<th>Status</td>
		<th></td>
	</tr>
	<c:forEach items="${deliveries}" var="delivery">
		<tr>
			<td><fmt:formatDate type="both" value="${delivery.startDateTime}" />  </td>
			<c:if test="${delivery.status == 'COMPLETE'}">
				<td><fmt:formatDate type="both" value="${delivery.endDateTime}" /></td>
			</c:if>
			<c:if test="${delivery.status != 'COMPLETE'}">
				<td></td>
			</c:if>
			<td>${delivery.status}</td>
			<td><a href="deliveries/${delivery.id}">View detail</a></td>
		</tr>
	</c:forEach>
</table>
