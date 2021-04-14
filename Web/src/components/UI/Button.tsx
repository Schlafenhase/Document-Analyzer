import React from "react";
import styled from "styled-components";

const StyledButton = styled.button`
  background-color: #1c448e;
  border: 0;
  border-radius: 4px;
  padding: 8px;
  color: white;
  font-weight: bold;
  cursor: pointer;
`;

const Button = (props: any) => {
    return <StyledButton {...props}>{props.children}</StyledButton>;
};

export default Button;
