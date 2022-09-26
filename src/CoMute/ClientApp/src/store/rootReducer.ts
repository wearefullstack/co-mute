import { combineReducers } from '@reduxjs/toolkit';
import { reducer as formReducer } from 'redux-form';
import { reducer as notificationReducer } from 'src/slices/notification';

const rootReducer = combineReducers({
  form: formReducer,
  notifications: notificationReducer
});

export default rootReducer;
