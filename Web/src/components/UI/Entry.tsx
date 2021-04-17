import React from "react";
import styled from "styled-components";

const StyledEntry = styled.input`
  background-color: #d5d5d5;
  border: 1.5px solid #f61067;
  border-radius: 2px;
  padding: 0 10px;
  color: #f61067;
  font-weight: bold;
  font-size: 12pt;
  height: 30px;
`;

const Entry = (props: any) => {
    return <StyledEntry placeholder={props.label}
                        {...props}
                        value={props.state[0]}
                        onChange={(e) => props.state[1](e.target.value)}>{props.children}</StyledEntry>;
};

export default Entry;
