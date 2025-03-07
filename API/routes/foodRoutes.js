// importing express module
import express from 'express'

// importing authenticate
import authenticate from '../middleware/authenticator.js';

// importing controllers
import { 
    getFoods, 
    getFood, 
    search, 
    createFood, 
    deleteFood, 
    updateFood } from '../controllers/foodController.js'

// creates new router from express module
const router = express.Router();

// defining post routes
router.get('/', authenticate, getFoods);
router.get('/search', authenticate, search);
router.get('/:id', authenticate, getFood);
router.post('/', authenticate, createFood);
router.delete('/:id', authenticate, deleteFood);
router.put('/:id', authenticate, updateFood);

// exports router as default
export default router;