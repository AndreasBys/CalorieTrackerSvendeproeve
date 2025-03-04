// importing express module
import express from 'express'
import authenticate from '../middleware/authenticator.js';
import { createMacroLog, deleteMacroLog, getTodaysMacroLog, updateMacroLog } from '../controllers/macroLogController.js';

// creates new router from express module
const router = express.Router();

// defining routes
router.get('/', authenticate, getTodaysMacroLog);
router.post('/', authenticate, createMacroLog);
router.delete('/:id', authenticate, deleteMacroLog);
router.patch('/:id', authenticate, updateMacroLog);

// exports router as default
export default router;