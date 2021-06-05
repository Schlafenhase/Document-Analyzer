import * as React from "react";
import {
    AppBar,
    Toolbar,
    IconButton,
    List,
    ListItem,
    ListItemText,
    Container
} from "@material-ui/core";
import {makeStyles, withStyles} from "@material-ui/core/styles";
import {useState} from "react";
import logo from "../../assets/da-logo.svg";
import styled from "styled-components";

const useStyles = makeStyles({
    navbarDisplayFlex: {
        display: `flex`,
        justifyContent: `space-between`
    },
    navDisplayFlex: {
        display: `flex`,
        justifyContent: `space-between`
    },
    linkText: {
        textDecoration: `none`,
        textTransform: `uppercase`,
        color: `white`
    }
});

const navLinks = [
    { title: `Files`, path: `/home` },
    { title: `Employees`, path: `/usernames` },
];

const SignOutListItem = withStyles((theme) => ({
    root: {
        border: "2px solid #f61067",
        "&$selected": {
            backgroundColor: "green",
            color: "white",
            "& .MuiListItemIcon-root": {
                color: "white"
            }
        },
        "&$selected:hover": {
            backgroundColor: "purple",
            color: "white",
            "& .MuiListItemIcon-root": {
                color: "white"
            }
        },
        "&:hover": {
            backgroundColor: theme.palette.secondary.main,
            color: "white",
            "& .MuiListItemIcon-root": {
                color: "white"
            }
        }
    },
    selected: {}
}))(ListItem);

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

const Header = (props: any) => {
    const classes = useStyles();
    const [currentPage, setCurrentPage] = useState("home");

    function signOut() {
        localStorage.removeItem("token");
        setCurrentPage("/");
        setTimeout(() => {
            window.location.reload();
        }, 1000);
    }

    return (
        <AppBar position="static">
            <Toolbar>
                <Container maxWidth="md" className={classes.navbarDisplayFlex}>
                    <div>
                        <Logo src={logo} />
                        <LogoWords>Document Analyzer</LogoWords>
                    </div>
                    <List
                        component="nav"
                        aria-labelledby="main navigation"
                        className={classes.navDisplayFlex}
                    >
                        {navLinks.map(({ title, path }) => (
                            <a href={path} key={title} className={classes.linkText}>
                                <ListItem button>
                                    <ListItemText primary={title} />
                                </ListItem>
                            </a>
                        ))}
                        <a key={"Sign Out"} className={classes.linkText} onClick={() => signOut()}>
                            <SignOutListItem button>
                                <ListItemText primary="Sign Out" />
                            </SignOutListItem>
                        </a>
                    </List>
                </Container>
            </Toolbar>
        </AppBar>
    );
};
export default Header;
