// importing express module
import express from 'express';

// importing controllers
import {
    login, 
    createUser} from '../controllers/authController.js'

// creates new router from express module
const router = express.Router();

// defining post routes
router.post('/login', login);
router.post('/register', createUser);

// exporting router as default
export default router;