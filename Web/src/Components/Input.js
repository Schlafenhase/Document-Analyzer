import React, { useState } from "react";
import styled from "styled-components";

const Container = styled.div`
  position: relative;
  display: flex;
  flex-direction: column;
`;

const Label = styled.label`
  position: absolute;
  top: -16px;
`;

const Input = (props) => {
  const [empty, setEmpty] = useState(true);
  return (
    <Container>
      <label style={{ opacity: empty ? 0 : 0.3 }}>{props.label}</label>
      <input
        {...props}
        placeholder={props.label}
        onChange={(e) => setEmpty(!e.target.value)}
      ></input>
    </Container>
  );
};

export default Input;
