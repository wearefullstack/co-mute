/* eslint no-param-reassign: "error" */
import React, { useState, useEffect} from 'react';
import type { FC } from 'react';
import PropTypes from 'prop-types';
import clsx from 'clsx';
import * as Yup from 'yup';
import { Formik } from 'formik';
import { useSnackbar } from 'notistack';
import { Button, Card, CardContent, Grid,
  TextField, makeStyles} from '@material-ui/core';
import axios from 'axios';
import useIsMountedRef from 'src/hooks/useIsMountedRef';
import { DEFAULT_IMAGE_URL } from 'src/constants';
import type { User } from 'src/types/user';

interface EditFormProps {
  className?: string;
  setEditMode: (mode: boolean) => void;
  editMode: boolean;
}

const useStyles = makeStyles(() => ({
  root: {},
  avatar:{
    border: "6px solid #ccc",
    borderRadius: '50%',
    justifyContent: 'center'
  },
  center: {
    textAlign: 'center',
  },
  formControl: {
    width: '100%'
  },
  inputLabel: {
    top: '-4.5px',
    left: '15px',
  }
}));


const EditForm: FC<EditFormProps> = ({className,setEditMode, editMode, ...rest}) => {
  const classes = useStyles();
  const { enqueueSnackbar } = useSnackbar();

  const isMountedRef = useIsMountedRef();
  const [user, setUser] = useState<User | null>(null);

  useEffect(() => {
    const _user: User = JSON.parse(localStorage.getItem('user'));
    setUser(_user);
  }, [setUser]);

  const updateLocalUser = (values:User) => {
    const newUser: User = JSON.parse(localStorage.getItem('user'));
    newUser.name = values.name;
    newUser.surname = values.surname;
    newUser.phone = values.phone;
    localStorage.setItem('user', JSON.stringify(newUser));
    window.location.reload();
  }
  return (
    <Formik
      enableReinitialize
      initialValues={{
        id: user?.id,
        name: user?.name ?? '',
        surname: user?.surname ?? '',
        email: user?.email ?? '',
        phone: user?.phone ?? '',
        submit: null,
      }}
      validationSchema={Yup.object().shape({
        name: Yup.string().min(4).max(20).required('Name is required'),
        surname: Yup.string().min(4).max(20).required('Surname is required'),
      })}
      onSubmit={async (values, {
        resetForm,
        setErrors,
        setStatus,
        setSubmitting,
      }) => {
        try {
          setEditMode(false);
          const endpoint = '/users/update';
          const data:User = {
            id: values.id,
            name: values.name,
            surname: values.surname,
            email: values.email,
            phone: values.phone
          };

          const response = await axios.post(endpoint, JSON.stringify(data));
          if (isMountedRef.current) {
            if (response.status === 200) {
              resetForm();
              setStatus({ success: true });
              setSubmitting(false);
              enqueueSnackbar('User updated successfully', { variant: 'success' });
              updateLocalUser(data); 
            } else {
              enqueueSnackbar('User updated failed', { variant: 'error' });
            }
          }
        } catch (err) {
          console.error(err);
          setStatus({ success: false });
          setErrors({ submit: "User update failed" });
          setSubmitting(false);
        }
      }}>
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
                <Grid item md={6} xs={12} className={classes.center}>
                  <img alt={"avatar"} src={DEFAULT_IMAGE_URL} className={classes.avatar}/>
                </Grid>
                <Grid item md={6} xs={12}>
                  <Grid container spacing={3}>
                    <Grid item md={6} xs={12}>
                      <TextField
                        error={Boolean(touched.name && errors.name)}
                        fullWidth
                        helperText={touched.name && errors.name}
                        label="Name"
                        name="name"
                        disabled={!editMode}
                        onBlur={handleBlur}
                        onChange={handleChange}
                        required
                        value={values.name}
                        variant="outlined"/>
                    </Grid>
                    <Grid item md={6} xs={12}>
                      <TextField
                        error={Boolean(touched.surname && errors.surname)}
                        fullWidth
                        helperText={touched.surname && errors.surname}
                        label="Surname"
                        name="surname"
                        disabled={!editMode}
                        onBlur={handleBlur}
                        onChange={handleChange}
                        required
                        value={values.surname}
                        variant="outlined"/>
                    </Grid>
                    <Grid item md={6} xs={12}>
                      <TextField
                        fullWidth
                        label="Email"
                        name="email"
                        disabled
                        value={values.email}
                        variant="outlined"/>
                    </Grid>
                    <Grid item md={6} xs={12}>
                      <TextField
                        error={Boolean(touched.phone && errors.phone)}
                        fullWidth
                        helperText={touched.phone && errors.phone}
                        label="Phone"
                        name="phone"
                        disabled={!editMode}
                        onBlur={handleBlur}
                        onChange={handleChange}
                        value={values.phone}
                        variant="outlined"/>
                    </Grid>
                    <Grid item xs={12}>
                      {editMode && 
                        <Button
                          variant="contained"
                          color="secondary"
                          type="submit"
                          style={{height: '56px'}}
                          disabled={isSubmitting}
                          fullWidth>
                          Save user
                        </Button>}
                    </Grid>
                  </Grid>
                </Grid>
              </Grid>
            </CardContent>
          </Card>
        </form>
      )}
    </Formik>
  );
};

EditForm.propTypes = {
  className: PropTypes.string,
};

export default EditForm;
