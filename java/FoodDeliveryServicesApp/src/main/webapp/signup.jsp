<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@ taglib uri="http://www.springframework.org/tags/form" prefix="form" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>

<script>
	function checkPassword(){
		var password = document.getElementById("password").value;
		var cPassword = document.getElementById("cPassword").value;
		if(password == cPassword){
			return true;
		}else{
			document.getElementById("cPasswordMsg").innerHTML = "Confirm password does not match!";
			return false;
		} 
	}
	
	function userTypeChanged(){
		var userType = document.getElementById("userType").value;
		var display = "none";
		if(userType == "supplier"){
			display = "table-row";
		}
		document.getElementById("deliveryRadiusTR").style.display = display;
	}
</script>

		<h1>
			<c:choose>
				<c:when test="${isEdit==null}">
					<c:set var="button" value="Signup"></c:set>
					Signup New User
				</c:when>
				<c:otherwise>
					<c:set var="button" value="Update"></c:set>
					Edit My Profile
				</c:otherwise>
			</c:choose>
		</h1>
		<form:form modelAttribute="user" onsubmit="return checkPassword()" method="POST">
			<form:hidden path="id"/>
			<table>
				<tr>
					<td>Full name</td>
					<td>
						<form:input path="fullName" />
					</td>
					<td>
						<form:errors path="fullName"></form:errors>
					</td>
				</tr>
				<tr>
					<td>Address</td>
					<td>
						<form:input path="address" />
					</td>
					<td>
						<form:errors path="address"></form:errors>
					</td>
				</tr>
				<tr>
					<td>Email</td>
					<td>
						<form:input path="email" />
					</td>
					<td>
						<form:errors path="email"></form:errors>
					</td>
				</tr>
				<tr>
					<td>Contact</td>
					<td>
						<form:input path="contact" />
					</td>
					<td>
						<form:errors path="contact" ></form:errors>
					</td>
				</tr>
				<tr>
					<td>Username</td>
					<td>
						<form:input path="userName" readonly="${isEdit}" />
					</td>
					<td>
						<form:errors path="userName"></form:errors>
					</td>
				</tr>
				<tr>
					<td>Password</td>
					<td>
						<form:password path="password" id="password" />
					</td>
					<td>
						<form:errors path="password"></form:errors>
					</td>
				</tr>
				<tr>
					<td>Confirm password</td>
					<td>
						<input type="password" id="cPassword" />
					</td>
					<td>
						<span id="cPasswordMsg"></span>
					</td>
				</tr>
				
				<c:choose>
					<c:when test="${isEdit==null}">
						<tr>
							<td>User type</td>
							<td>
								<form:select path="userType" onchange="userTypeChanged()" id="userType">
									<form:option value="supplier">Supplier</form:option>
									<form:option value="consumer">Consumer</form:option>
								</form:select>
							</td>
							<td></td>
						</tr>
					</c:when>
					<c:otherwise>
						<tr><td><form:hidden path="userType"/></td></tr>
					</c:otherwise>
				</c:choose>
				
				<c:if test="${user.userType != 'customer'}">
					<tr id="deliveryRadiusTR">
						<td>Delivery radius</td>
						<td>
							<form:input path="deliveryRadius"/>
						</td>
						<td></td>
					</tr>
				</c:if>
				<tr>
					<td>
						<input type="submit" value="${button}" />
					</td>
				</tr>
			</table>
		</form:form>
	