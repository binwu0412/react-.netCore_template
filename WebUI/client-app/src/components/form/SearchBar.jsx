import React from "react";
import { Paper, InputBase, IconButton, makeStyles } from "@material-ui/core";
import { Search, Person } from '@material-ui/icons';

const useStyles = makeStyles(() => ({
  root: {
    padding: "2px 4px",
    display: "flex",
    alignItems: "center",
    width: 400,
    margin: "10px 0" 
  },
  input: {
    marginLeft: 10,
    flex: 1,
  },
  iconButton: {
    padding: 10,
  },
  divider: {
    height: 28,
    margin: 4,
  },
}));

const SearchBar = ({onClick, searchString, onChange}) => {
  const classes = useStyles();
  return (
    <Paper component="form" className={classes.root}>
      <IconButton className={classes.iconButton} disabled>
        <Person />
      </IconButton>
      <InputBase
        value={searchString}
        className={classes.input}
        placeholder="Search Employee"
        onChange={onChange} />
      <IconButton className={classes.iconButton} onClick={onClick}>
        <Search />
      </IconButton>
    </Paper>
  );
}

export default SearchBar;