// importing express module
import express from 'express'
import authenticate from '../middleware/authenticator.js';
import { createMacroLog, deleteMacroLog, getTodaysMacroLog, updateMacroLog } from '../controllers/macroLogController.js';
import { getMacroLogDays } from '../controllers/macroLogLegacyController.js';

// creates new router from express module
const router = express.Router();

// defining routes
// legacy routes
router.get('/search', authenticate, getMacroLogDays);

// normal routes
router.get('/', authenticate, getTodaysMacroLog);
router.post('/', authenticate, createMacroLog);
router.delete('/:id', authenticate, deleteMacroLog);
router.patch('/:id', authenticate, updateMacroLog);

// exports router as default
export default router;