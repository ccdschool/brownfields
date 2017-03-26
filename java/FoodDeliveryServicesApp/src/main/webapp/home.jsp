<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/functions" prefix="fn"%>
<%@ taglib uri="http://www.springframework.org/security/tags" prefix="sec" %>
<%@ taglib uri="http://www.springframework.org/tags/form" prefix="form"%>

<h3>Foods</h3>
<center>
	<table>
		<c:forEach items="${items}" var="row" varStatus="rowCounter">
			<c:if test="${rowCounter.count % 3 == 1}">
				<tr>
			</c:if>
			<td>
				<c:if test="${empty row.imgUrl}">
					<img class="no_image" src="<c:url value="/resources/img/no-image.png" />" /> 
				</c:if>
				<c:if test="${not empty row.imgUrl}">
					<img width="100px" height="100px" src="<c:url value="${row.imgUrl}" />"/>
				</c:if>
				<br />${row.name}
				$ ${row.price }<br />
				
				<sec:authorize access="!hasRole('ROLE_ADMIN') and !hasRole('ROLE_SUPPLIER')">
					<form:form action="addToCart" modelAttribute="item" method="post">
					<form:hidden path="id" value="${row.id}" /> 
					<form:hidden path="name" value="${row.name}" /> 
					<form:hidden path="price" value="${row.price}" /> 
					<form:hidden path="description" value="${row.description}" /> 
					<form:hidden path="supplier.id" value="${row.supplier.id}" /> 
					<input type="submit" value="Add to Cart">
				</form:form>
				</sec:authorize>
			</td>
			<c:if test="${rowCounter.count % 3 == 0||rowCounter.count == fn:length(values)}">
				</tr>
			</c:if>
		</c:forEach>
	</table>
</center>