// importing express module
import express from 'express'

// importing authentcate 
import authenticate from '../middleware/authenticator.js';

// importing controllers
import { deleteUser, updateUser, getUser } from '../controllers/userController.js'

// creates new router from express module
const router = express.Router();

// defining post routes
router.get('/:id', authenticate, getUser);
router.patch('/:id', authenticate, updateUser);
router.delete('/:id', authenticate, deleteUser);

// exporting router as default
export default router;