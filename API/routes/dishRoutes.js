// importing express module
import express from 'express'


// importing controllers
import { getAllUserDishes, getDish, createDish, deleteDish, updateDish } from '../controllers/dishController.js'
import authenticate from '../middleware/authenticator.js';

// creates new router from express module
const router = express.Router();

// defining post routes
router.get('/', authenticate, getAllUserDishes);
router.get('/:id', authenticate, getDish);
router.post('/', authenticate, createDish);
router.delete('/:id', authenticate, deleteDish);
router.patch('/:id', authenticate, updateDish);

// exports router as default
export default router;