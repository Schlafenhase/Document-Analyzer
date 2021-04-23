import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import styled from "styled-components";
import fileIcon from "../../assets/file-icon.svg";
import userIcon from "../../assets/user-icon.svg";
import logo from "../../assets/da-logo.svg";

const Div = styled.nav`
  background: linear-gradient(
    0deg,
    rgba(222, 222, 222, 1) 0%,
    rgba(73, 122, 159, 1) 15%
  );
  color: white;
  display: flex;
  gap: 16px;

  a {
    color: inherit;
  }

  padding: 8px;
`;

const HeaderButton = styled.button`
  background-color: #184f81;
  border: 2px solid #43c59e;
  padding: 0 10px;
  color: white;
  font-weight: bold;
  cursor: pointer;
  font-size: 13pt;
  height: 40px;
  margin-top: 5px;

  img {
    height: 20px;
    filter: invert();
    margin-left: 10px;
  }
`;

const SignOutButton = styled.button`
  background-color: #184f81;
  border: 2px solid #f61067;
  padding: 0 10px;
  color: white;
  font-weight: bold;
  cursor: pointer;
  font-size: 13pt;
  height: 40px;
  margin-top: 5px;

  img {
    height: 20px;
    filter: invert();
    margin-left: 10px;
  }
`;

const LogoWords = styled.h1`
  font-size: 24pt;
  float: right;
  margin-left: 20px;
  margin-top: 5px;
  margin-right: 20px;
`;

const Logo = styled.img`
  height: 50px;
  margin-top: 5px;
  margin-left: 5px;
`;

const HeaderBar = (props: any) => {
  const [currentPage, setCurrentPage] = useState("home");

  function signOut() {
    localStorage.removeItem("token");
    setCurrentPage("/");
    setTimeout(() => {
      window.location.reload();
    }, 1000);
  }

  return (
    <Div>
      <div>
        <Logo src={logo} />
        <LogoWords>Document Analyzer</LogoWords>
      </div>
      <Link to="/home" onClick={() => setCurrentPage("home")}>
        <HeaderButton
          style={{
            backgroundColor: currentPage === "home" ? "#505050" : "#184f81",
          }}
        >
          Files
          <img src={fileIcon} />
        </HeaderButton>
      </Link>
      <Link to="/usernames" onClick={() => setCurrentPage("employees")}>
        <HeaderButton
          style={{
            backgroundColor:
              currentPage === "employees" ? "#505050" : "#184f81",
          }}
        >
          Employees
          <img src={userIcon} />
        </HeaderButton>
      </Link>
      <Link onClick={() => signOut()} to="/">
        <SignOutButton>Cerrar sesión</SignOutButton>
      </Link>
    </Div>
  );
};

export default HeaderBar;
