import React from "react";
import styled from "styled-components";

const StyledButton = styled.button`
  background-color: #184f81;
  border: 2px solid #ffca0a;
  border-radius: 10px;
  padding: 0 10px;
  color: white;
  font-weight: bold;
  cursor: pointer;
  font-size: 13pt;
  height: 40px;
  
  img {
    height: 20px;
    filter: invert();
    margin-left: 10px;
  }
`;

const CustomButton = (props: any) => {
    return <StyledButton {...props}>{props.children}</StyledButton>;
};

export default CustomButton;
