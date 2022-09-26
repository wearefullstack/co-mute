import React, { useState } from 'react';
import type { FC } from 'react';
import { useHistory } from 'react-router-dom';
import PropTypes from 'prop-types';
import clsx from 'clsx';
import * as Yup from 'yup';
import { Formik } from 'formik';
import { useSnackbar } from 'notistack';
import {
  Box,
  Button,
  Card,
  CardContent,
  Grid,
  TextField,
  makeStyles,
  FormControl,
  InputLabel,
  MenuItem,
  OutlinedInput,
  Select,
  useTheme,
} from '@material-ui/core';
import axios from 'axios';
import type { User } from 'src/types/user';
import { DAYS_AVAILABLE, TIMES } from 'src/constants';
import Label from 'src/components/Label';

interface CarPoolAddFormProps {
  className?: string;
}

const useStyles = makeStyles(() => ({
  root: {},
  formControl: {
    width: '100%'
  },
  inputLabel: {
    top: '-4.5px',
    left: '15px',
  },
  error:{
    color: '#f44336',
    fontSize: '0.75rem',
    background: 'transparent',
    justifyContent: 'left',
    textTransform: 'none',
    fontWeight: 'normal',
    marginTop: 3,
    marginLeft: 5
  }
}));

const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
  PaperProps: {
    style: {
      maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
      width: 250,
    },
  },
};

function getStyles(index: any, list: string | any[], theme: { typography: { fontWeightRegular: any; fontWeightMedium: any; }; }) {
  return {
    fontWeight:
      list.indexOf(index) === -1
        ? theme.typography.fontWeightRegular
        : theme.typography.fontWeightMedium,
  };
}

const CarPoolAddForm: FC<CarPoolAddFormProps> = ({
  className,
  ...rest
}) => {
  const theme = useTheme();
  const classes = useStyles();
  const { enqueueSnackbar } = useSnackbar();
  const history = useHistory();
  const [selectedDaysAvailable, setSelectedDaysAvailable] = useState<string[] | null>([]);
  const [departureTime, setDepartureTime] = useState<string | null>();
  const [arrivalTime, setArrivalTime] = useState<string | null>();
  const [departureTimeError, setDepartureTimeError] = useState<boolean>(false);
  const [arrivalTimeError, setArrivalTimeError] = useState<boolean>(false);

  const handleDaysAvailableChange = (event: { target: { value: any; }; }) => {
    const {
      target: { value },
    } = event;
    setSelectedDaysAvailable(
      typeof value === 'string' ? value.split(',') : value,
    );
  };
  
  const handleArrivalTimeChange = (event: { target: { value: any; }; }) => {
    const {
      target: { value },
    } = event;
    setArrivalTime(value);
    setArrivalTimeError(false);
  };

  const handleDepartureTimeChange = (event: { target: { value: any; }; }) => {
    const {
      target: { value },
    } = event;
    setDepartureTime(value);
    setDepartureTimeError(false);
  };

  const handleSelectValidations = ():boolean =>{
    let isValid:boolean = false;

    if(!departureTime){
      setDepartureTimeError(true);
      isValid = false;
    }else{
      setDepartureTimeError(false);
      isValid = true;
    }
    if(!arrivalTime){
      setArrivalTimeError(true);
      isValid = false;
    }else{
      setArrivalTimeError(false);
      isValid = true;
    }

    return isValid;
  }

  return (
    <Formik
      initialValues={{
        departureTime: "",
        arrivalTime: "",
        origin: "",
        daysAvailable: "",
        destination: "",
        availableSeats: "",
        notes: "",
        submit: null,
      }}
      validationSchema={Yup.object().shape({
        origin: Yup.string().min(2).max(20).required('Origin is required'),
        destination: Yup.string().min(2).max(20).required('Destination is required'),
        availableSeats: Yup.number().required("Available seats number is required")
      })}
      onSubmit={async (values, {
        resetForm,
        setErrors,
        setStatus,
        setSubmitting
      }) => {
        try {
          const user: User = JSON.parse(localStorage.getItem("user"));
          const endpoint = '/carpools/add';
          const isValid = handleSelectValidations();
          if(!isValid){
            setSubmitting(false);
            throw(new Error(""));
          }

          const data = JSON.stringify({
            departureTime: departureTime,
            arrivalTime: arrivalTime,
            origin: values.origin,
            ownerId: user.id,
            daysAvailable: selectedDaysAvailable.join(", "),
            destination: values.destination,
            availableSeats: values.availableSeats,
            notes: values.notes,
          });
          
          await axios.post(endpoint, data);
          resetForm();
          setStatus({ success: true });
          setSubmitting(false);
          enqueueSnackbar('Car pool was added successfully', { variant: 'success' });
          history.push('/app/carpools');
        } catch (err) {
          setStatus({ success: false });
          setErrors({ submit: 'Something went wrong, error while adding car pool' });
          setSubmitting(false);
        }
      }}
    >
      {({
        errors,
        handleBlur,
        handleChange,
        handleSubmit,
        isSubmitting,
        touched,
        values,
      }) => (
        <form
          noValidate
          className={clsx(classes.root, className)}
          onSubmit={handleSubmit}
          {...rest}>
          <Card>
            <CardContent>
              <Grid container spacing={3}>
              <Grid item md={6} xs={12}>
                  <TextField
                    error={Boolean(touched.origin && errors.origin)}
                    fullWidth
                    helperText={touched.origin && errors.origin}
                    label="Origin"
                    name="origin"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    required
                    value={values.origin}
                    variant="outlined"/>
                </Grid>
                <Grid item md={6} xs={12}>
                  <TextField
                    error={Boolean(touched.destination && errors.destination)}
                    fullWidth
                    helperText={touched.destination && errors.destination}
                    label="Destination"
                    name="destination"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    required
                    value={values.destination}
                    variant="outlined"/>
                </Grid>
                <Grid item md={6} xs={12}>
                  <TextField
                    error={Boolean(touched.availableSeats && errors.availableSeats)}
                    fullWidth
                    helperText={touched.availableSeats && errors.availableSeats}
                    label="Available seats"
                    name="availableSeats"
                    type="number"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    required
                    value={values.availableSeats}
                    variant="outlined"/>
                </Grid>
                <Grid item md={6} xs={12}>
                    <FormControl className={classes.formControl}>
                      <InputLabel id="days-available-label" className={classes.inputLabel}>Days available</InputLabel>
                      <Select
                        labelId="days-available-label"
                        id="days-available-select"
                        multiple
                        fullWidth
                        value={selectedDaysAvailable}
                        onChange={handleDaysAvailableChange}
                        input={<OutlinedInput label="Days available" />}
                        MenuProps={MenuProps}>
                        {DAYS_AVAILABLE.map((day) => (
                          <MenuItem
                            key={day.id}
                            value={day.value}
                            style={getStyles(day.id, selectedDaysAvailable, theme)}>
                            {day.value}
                          </MenuItem>
                        ))}
                      </Select>
                    </FormControl>
                </Grid>
                <Grid item md={6} xs={12}>
                  <FormControl className={classes.formControl}>
                    <InputLabel id="departure-time-label" error={departureTimeError} className={classes.inputLabel}>Departure time *</InputLabel>
                    <Select
                      labelId="departure-time-label"
                      id="departure-time-label"
                      fullWidth
                      required
                      onBlur={handleBlur}
                      error={departureTimeError}
                      value={departureTime}
                      onChange={handleDepartureTimeChange}
                      input={<OutlinedInput label="Departure time *" />}
                      MenuProps={MenuProps}>
                      {TIMES.map((time) => (
                        <MenuItem key={time.id} value={time.value}>
                          {time.value}
                        </MenuItem>
                      ))}
                    </Select>
                    {departureTimeError && <Label className={classes.error}>Departure time is required</Label>}
                  </FormControl>
                </Grid>
                <Grid item md={6} xs={12}>
                    <FormControl className={classes.formControl}>
                      <InputLabel id="arrival-time-label" error={arrivalTimeError} className={classes.inputLabel}>Arrival time *</InputLabel>
                      <Select
                        labelId="arrival-time-label"
                        id="arrival-time-label"
                        fullWidth
                        required
                        onBlur={handleBlur}
                        error={arrivalTimeError}
                        value={arrivalTime}
                        onChange={handleArrivalTimeChange}
                        input={<OutlinedInput label="Arrival time *" />}
                        MenuProps={MenuProps}>
                        {TIMES.map((time) => (
                          <MenuItem key={time.id} value={time.value}>
                            {time.value}
                          </MenuItem>
                        ))}
                      </Select>
                    {arrivalTimeError && <Label className={classes.error}>Arrival time is required</Label>}
                    </FormControl>
                </Grid>
                <Grid item md={12} xs={12}>
                  <TextField
                    error={Boolean(touched.notes && errors.notes)}
                    fullWidth
                    helperText={touched.notes && errors.notes}
                    label="Notes"
                    name="notes"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    multiline
                    rowsMax={4}
                    value={values.notes}
                    variant="outlined"/>
                </Grid>
              </Grid>
              <Box mt={2}>
                <Button
                  variant="contained"
                  color="secondary"
                  type="submit"
                  disabled={isSubmitting}
                  fullWidth>
                  Add car pool
                </Button>
              </Box>
            </CardContent>
          </Card>
        </form>
      )}
    </Formik>
  );
};

CarPoolAddForm.propTypes = {
  className: PropTypes.string,
};

export default CarPoolAddForm;
