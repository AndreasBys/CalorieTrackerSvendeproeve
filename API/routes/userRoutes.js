// importing express module
import express from 'express'

// importing authentcate 
import { authenticate } from '../middleware/authenticator.js';

// importing controllers
import { getUsers, createUser, deleteUser } from '../controllers/userController.js'

// creates new router from express module
const router = express.Router();

// defining post routes
router.get('/', authenticate, getUsers);
router.post('/', authenticate, createUser);
router.delete('/:id', authenticate, deleteUser);

// exporting router as default
export default router;


